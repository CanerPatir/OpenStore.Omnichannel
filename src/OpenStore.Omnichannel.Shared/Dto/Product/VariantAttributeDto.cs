using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class VariantAttributeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<VariantAttributeValueDto> Values { get; set; }
    }
}