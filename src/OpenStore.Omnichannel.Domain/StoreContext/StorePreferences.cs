namespace OpenStore.Omnichannel.Domain.StoreContext
{
    public class StorePreferences : AuditableEntity
    {
        public string Name { get; set; }
        public virtual StorePreferencesContact Contact { get; set; }

        public StorePreferences()
        {
            Contact = new StorePreferencesContact();
        }
    }
}