using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Query;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql;

public class GetProductDetailByHandleQueryHandler : IRequestHandler<GetProductDetailByHandleQuery, ProductDetailResult>
{
    private readonly ICrudRepository<Product> _repository;

    public GetProductDetailByHandleQueryHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<ProductDetailResult> Handle(GetProductDetailByHandleQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.Query
            .Include(x => x.Variants)
                .ThenInclude(x => x.Inventory)
            .Include(x => x.Medias)
            .FirstOrDefaultAsync(x => x.Handle == request.Handle, cancellationToken);

        if (product is null)
        {
            return null;
        }

        return new ProductDetailResult(
            product.Handle,
            product.Id,
            product.Title,
            product.Description,
            product.MetaTitle,
            product.MetaDescription,
            product.HasMultipleVariants,
            product.IsPhysicalProduct,
            product.Medias.Select(x => new ProductDetailMediaDto(
                x.Id,
                x.Host,
                x.Path,
                x.Type,
                x.Title,
                x.Extension,
                x.Filename,
                x.Position,
                x.Size,
                x.Url,
                x.VariantIds.ToHashSet()
            )).ToList(),
            product.Variants.Select(x => new ProductDetailVariantDto(
                x.Id,
                x.Price,
                x.CompareAtPrice,
                x.CalculateTaxAdditionally,
                x.Sku,
                x.Barcode,
                x.TrackQuantity,
                x.Inventory.ContinueSellingWhenOutOfStock,
                x.Inventory.Quantity,
                x.Option1, 
                x.Option2, 
                x.Option3,
                x.Title
            )).ToList()
        );
    }
}