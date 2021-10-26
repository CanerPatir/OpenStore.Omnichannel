using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Shared.Request;

public record MakeProductAsMultiVariantRequest(IEnumerable<ProductOptionDto> Options, IEnumerable<VariantDto> Variants);