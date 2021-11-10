using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Shared.Request;

public record MakeProductAsMultiVariantRequest(IEnumerable<ProductOptionDto> Options, IEnumerable<VariantDto> Variants);