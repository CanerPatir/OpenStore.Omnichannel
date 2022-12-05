using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class AssignProductMediaHandler : ICommandHandler<AssignProductMedia, IEnumerable<ProductMediaDto>>
{
    private readonly IMediator _mediator;
    private readonly ICrudRepository<ProductMedia> _productMediaRepository;
    private readonly ICrudRepository<Product> _repository;

    public AssignProductMediaHandler(IMediator mediator
        , ICrudRepository<Product> repository
        , ICrudRepository<ProductMedia> productMediaRepository)
    {
        _mediator = mediator;
        _repository = repository;
        _productMediaRepository = productMediaRepository;
    }

    public async Task<IEnumerable<ProductMediaDto>> Handle(AssignProductMedia command, CancellationToken cancellationToken)
    {
        var (id, fileUploads) = command;
        var list = (await _mediator.Send(new CreateProductMedia(fileUploads), cancellationToken)).ToList();
        var product = await _repository.Query
            .Include(x => x.Medias)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            throw new ResourceNotFoundException(Msg.ResourceNotFound);
        }

        foreach (var dto in list)
        {
            var productMedia = await _productMediaRepository.GetAsync(dto.Id, cancellationToken);
            product.AssignAttachedMedia(productMedia, dto);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return list;
    }
}