using OpenStore.Omnichannel.Shared.Dto.Management.Store;

namespace OpenStore.Omnichannel.Domain.StoreContext;

public class StorePreferences : AuditableEntity
{
    public string Name { get; set; }
    public virtual StorePreferencesContact Contact { get; set; }

    public StorePreferences()
    {
        Contact = new StorePreferencesContact();
    }

    public void Update(StorePreferencesDto model)
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