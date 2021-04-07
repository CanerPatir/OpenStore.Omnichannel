using System;
using OpenStore.Domain;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class ProductMedia : Entity<Guid>
    {
        public Guid ProductId { get; protected set; }
        public int Order { get; protected set; }
        public string Path { get; protected set; }
        public int? Width { get; protected set; }
        public int? Height { get; protected set; }
        // Ratio 
        // 287 * 430
        // 415 * 622

        public ProductMedia(Guid productId, string path, int? width, int? height)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Path = path;
            Width = width;
            Height = height;
        }

        public void ChangeOrder(int order)
        {
            Order = order;
        }
    }
}