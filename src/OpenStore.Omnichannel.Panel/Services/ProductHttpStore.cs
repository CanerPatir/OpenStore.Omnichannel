using System.Net.Http;

namespace OpenStore.Omnichannel.Panel.Services
{
    public class ProductHttpStore : HttpStore
    {
        protected override string Path => "api/products";

        public ProductHttpStore(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}