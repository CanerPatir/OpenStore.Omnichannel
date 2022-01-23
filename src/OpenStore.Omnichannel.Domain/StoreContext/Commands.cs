using OpenStore.Omnichannel.Shared.Command;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

namespace OpenStore.Omnichannel.Domain.StoreContext;

public record UpdateStorePreferences(StorePreferencesQueryResult Model) : ICommand;