using System;
using OpenStore.Omnichannel.Domain.ProductContext;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace OpenStore.Omnichannel.Domain.ChannelContext
{
    public class SaleChannelProduct
    {
        public SaleChannelProduct(Guid saleChannelId, Guid productId)
        {
            ProductId = productId;
            SaleChannelId = saleChannelId;
        }

        public Guid ProductId { get; protected set; }
        public virtual Product Product { get; protected set; }

        public Guid SaleChannelId { get; protected set; }
        public virtual SaleChannel SaleChannel { get; protected set; }
    }
}