namespace OpenStore.Omnichannel.Shared.Dto.Management.Product;

public class ProductOptionDto
{
    public string Name { get; set; }
    public HashSet<string> Values { get; set; } = new();
}