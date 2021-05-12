using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class ProductOption
    {
        public string Name { get; protected set; }

        public HashSet<string> Values { get; protected set; }
        
        [JsonConstructor]
        public ProductOption(string name, HashSet<string> values)
        {
            Name = name;
            Values = values.ToHashSet(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}