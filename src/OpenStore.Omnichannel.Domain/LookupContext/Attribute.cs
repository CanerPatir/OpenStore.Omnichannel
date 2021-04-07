using System;
using System.Collections.Generic;
using System.Linq;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.LookupContext
{
    public class Attribute : LookupEntity
    {
        public AttributeType Type { get; set; }
        public virtual ICollection<AttributeValue> Values { get; set; } = new List<AttributeValue>();

         public AttributeValue FindAttributeValue(Guid attributeValueId)
        {
            return Values
                .SingleOrDefault(x => x.Id == attributeValueId) ?? throw new DomainException(Msg.Domain.GivenAttrValueNotBelongAttr);
        }
    }
}