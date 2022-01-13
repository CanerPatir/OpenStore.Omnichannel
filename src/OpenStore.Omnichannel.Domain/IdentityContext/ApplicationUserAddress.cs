using Microsoft.AspNetCore.Identity;

namespace OpenStore.Omnichannel.Domain.IdentityContext;

public class ApplicationUserAddress : AuditableEntity
{
    public Guid ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
    
    [PersonalData]  public string Firstname { get; set; }
    [PersonalData]  public string Surname { get; set; }
    [PersonalData]  public string PhoneNumber { get; set; }
    [PersonalData]  public string City { get; set; }
    [PersonalData]  public string Town { get; set; }
    [PersonalData]  public string District { get; set; }
    [PersonalData]  public string AddressDescription { get; set; }
    [PersonalData]  public string PostCode { get; set; }
    [PersonalData]  public string AddressName { get; set; }
}