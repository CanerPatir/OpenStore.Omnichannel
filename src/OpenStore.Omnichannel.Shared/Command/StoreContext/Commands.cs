using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

namespace OpenStore.Omnichannel.Shared.Command.StoreContext;

public record UpdateStorePreferences(StorePreferencesQueryResult Model) : ICommand;