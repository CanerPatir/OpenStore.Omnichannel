using OpenStore.Omnichannel.Shared.DomainEvents.StoreContext;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

namespace OpenStore.Omnichannel.Domain.StoreContext;

public class StorePreferences : AuditableEntity
{
    public string Name { get; protected set; }

    public CurrencyCode DefaultCurrency { get; protected set; }
    
    public virtual StorePreferencesContact Contact { get; protected set; }

    public StorePreferences()
    {
        Contact = new StorePreferencesContact();
    }

    public void Update(StorePreferencesQueryResult model)
    {
        Name = model.Name;
        Contact = new StorePreferencesContact
        {
            Email = model.Contact.Email,
            Address = model.Contact.Address,
            Phone = model.Contact.Phone,
            CopyrightText = model.Contact.CopyrightText,
            FacebookUrl = model.Contact.FacebookUrl,
            InstagramUrl = model.Contact.InstagramUrl,
            TwitterUrl = model.Contact.TwitterUrl,
            YoutubeUrl = model.Contact.YoutubeUrl,
        };
            
        ApplyChange(new StorePreferencesUpdated(Id, model));
    }
}