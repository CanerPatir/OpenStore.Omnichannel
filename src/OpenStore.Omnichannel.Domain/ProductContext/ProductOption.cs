using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class ProductOption
    {
        public string Name { get; protected set; }

        public IEnumerable<string> Values { get; protected set; }
        
        [JsonConstructor]
        public ProductOption(string name, IEnumerable<string> values)
        {
            Name = name;
            Values = values;
        }
    }
}