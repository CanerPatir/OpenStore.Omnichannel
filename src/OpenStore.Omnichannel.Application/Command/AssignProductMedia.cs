using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Application.Command
{
    public record AssignProductMedia(Guid Id, IEnumerable<FileUploadDto> Uploads) : IRequest<IEnumerable<ProductMediaDto>>;

    public class AssignProductMediaHandler : IRequestHandler<AssignProductMedia, IEnumerable<ProductMediaDto>>
    {
        private readonly IMediator _mediator;
        private readonly ICrudRepository<Product> _repository;

        public AssignProductMediaHandler(IMediator mediator, ICrudRepository<Product> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<IEnumerable<ProductMediaDto>> Handle(AssignProductMedia request, CancellationToken cancellationToken)
        {
            var (id, fileUploads) = request;
            var list = (await _mediator.Send(new CreateProductMedia(fileUploads), cancellationToken)).ToList();
            var product = await  _repository.Query
                .Include(x => x.Medias)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            
            foreach (var (dto, productMedia) in list)
            {
                product.AssignAttachedMedia(productMedia, dto);
            }
            await _repository.SaveChangesAsync(cancellationToken);
            return list.Select(x => x.dto);
        }
    }
}