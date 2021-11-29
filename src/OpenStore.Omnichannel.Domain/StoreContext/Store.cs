namespace OpenStore.Omnichannel.Domain.StoreContext;

public class Store : AuditableEntity
{
    public string Name { get; protected set; }

    public string Url { get; protected set; }

    public bool SslEnabled { get; protected set; }

    public string Hosts { get; protected set; }

    public string DefaultCulture { get; protected set; }

    public string CompanyName { get; protected set; }

    public string CompanyAddress { get; protected set; }

    public string CompanyPhoneNumber { get; protected set; }

    public string CompanyVat { get; protected set; }
}