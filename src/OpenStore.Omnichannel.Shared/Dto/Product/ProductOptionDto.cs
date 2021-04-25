using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductOptionDto
    {
         public string Name { get; set; }
         public List<string> Values { get; set; } = new();
    }
}