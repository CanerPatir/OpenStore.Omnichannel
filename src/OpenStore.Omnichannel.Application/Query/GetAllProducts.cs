using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Application.Extensions;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.ReadModel;

namespace OpenStore.Omnichannel.Application.Query
{
    public record GetAllProducts(int? PageNumber, int? PageSize) : IRequest<PagedList<ProductListItemReadModel>>;

    public class GetAllProductsHandler : IRequestHandler<GetAllProducts, PagedList<ProductListItemReadModel>>
    {
        private readonly ICrudRepository<Product> _repository;

        public GetAllProductsHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public Task<PagedList<ProductListItemReadModel>> Handle(GetAllProducts request, CancellationToken cancellationToken)
        {
            var (pageNumber, pageSize) = request;
            return _repository.Query
                .Include(x => x.Medias)
                .Include(x => x.Variants)
                .ThenInclude(x => x.Inventory)
                .GetPaged(
                    pageNumber,
                    pageSize,
                    p => new ProductListItemReadModel(
                        p.Id,
                        p.Medias.FirstOrDefault()?.Url,
                        p.Status,
                        p.Title,
                        p.Variants.Select(x => x.Inventory).Where(x => x is not null).Sum(x => x.AvailableQuantity),
                        p.HasMultipleVariants,
                        p.Variants.Count,
                        p.IsPhysicalProduct
                    ),
                    cancellationToken
                );
        }
    }
}