

// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel;

public record ErrorReadModel(string Message, IEnumerable<string> Errors);