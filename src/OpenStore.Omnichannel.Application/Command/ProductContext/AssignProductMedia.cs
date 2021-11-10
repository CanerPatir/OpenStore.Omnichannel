using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

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

    public async Task<IEnumerable<ProductMediaDto>> Handle(AssignProductMedia command, CancellationToken cancellationToken)
    {
        var (id, fileUploads) = command;
        var list = (await _mediator.Send(new CreateProductMedia(fileUploads), cancellationToken)).ToList();
        var product = await _repository.Query
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