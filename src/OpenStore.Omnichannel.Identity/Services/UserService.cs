using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Core;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Data.EntityFramework.Crud;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Shared.Dto.Identity;
using OpenStore.Omnichannel.Shared.Request.IdentityContext;

namespace OpenStore.Omnichannel.Identity.Services;

public class UserService : EntityFrameworkCrudService<ApplicationUser, ApplicationUserDto>, IUserService
{
    private readonly OpenIddictTokenManager<ApplicationToken> _tokenManager;
    private readonly OpenIddictAuthorizationManager<ApplicationAuthorization> _authorizationManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICrudRepository<ApplicationUser> _repository;

    public UserService(
        OpenIddictTokenManager<ApplicationToken> tokenManager,
        OpenIddictAuthorizationManager<ApplicationAuthorization> authorizationManager,
        UserManager<ApplicationUser> userManager,
        ICrudRepository<ApplicationUser> repository,
        IOpenStoreObjectMapper mapper) : base(repository, mapper)
    {
        _tokenManager = tokenManager;
        _authorizationManager = authorizationManager;
        _userManager = userManager;
        _repository = repository;
    }

    public async Task AddToRole(Guid userId, string role, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        var result = await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));
        }
    }

    public async Task RemoveFromRole(Guid userId, string role, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        var result = await _userManager.RemoveFromRoleAsync(user, role);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));
        }
    }

    public async Task<IEnumerable<string>> GetUserRoles(Guid id, CancellationToken cancellationToken = default)
    {
        return await _userManager.GetRolesAsync(new ApplicationUser
        {
            Id = id
        });
    }

    /// <summary>
    /// Invalidates tokens and security stamp
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task RevokeUserToken(Guid userId, CancellationToken cancellationToken = default)
    {
        await foreach (var auth in _authorizationManager.FindBySubjectAsync(userId.ToString(), cancellationToken))
        {
            await _authorizationManager.DeleteAsync(auth, cancellationToken);
        }

        await foreach (var token in _tokenManager.FindBySubjectAsync(userId.ToString(), cancellationToken))
        {
            await _tokenManager.TryRevokeAsync(token, cancellationToken);
        }

        // await _userManager.UpdateSecurityStampAsync(await _userManager.FindByIdAsync(userId.ToString())); 
    }

    public async Task ChangePassword(Guid userId, ChangePasswordRequest model, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            throw new ApplicationException(string.Join(",", Msg.Application.PasswordChangeError, result.Errors.Select(x => x.Description)));
        }
    }

    public async Task<IEnumerable<ApplicationUserAddressDto>> GetAddresses(Guid userId, CancellationToken cancellationToken)
    {
        var applicationUser = await _repository.GetAsync(userId, cancellationToken);
        return applicationUser
            .Addresses
            .Select(x => new ApplicationUserAddressDto(
                x.Id
                , x.Firstname
                , x.Surname
                , x.PhoneNumber
                , x.City
                , x.Town
                , x.District
                , x.AddressDescription
                , x.PostCode
                , x.AddressName));
    }

    public async Task AddAddress(Guid userId, ApplicationUserAddressDto model, CancellationToken cancellationToken)
    {
        var applicationUser = await _repository
            .Query
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        applicationUser
            .Addresses
            .Add(new ApplicationUserAddress
            {
                Firstname = model.Firstname,
                Surname = model.Surname,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                District = model.District,
                Town = model.Town,
                AddressDescription = model.AddressDescription,
                AddressName = model.AddressName,
                PostCode = model.PostCode
            });

        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAddress(Guid userId, ApplicationUserAddressDto model, CancellationToken cancellationToken)
    {
        var applicationUser = await _repository
            .Query
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        var address = applicationUser
            .Addresses
            .FirstOrDefault(x => x.Id == model.Id);

        if (address is null)
        {
            throw new ResourceNotFoundException(Msg.ResourceNotFound);
        }

        address.Firstname = model.Firstname;
        address.Surname = model.Surname;
        address.PhoneNumber = model.PhoneNumber;
        address.City = model.City;
        address.District = model.District;
        address.Town = model.Town;
        address.AddressDescription = model.AddressDescription;
        address.AddressName = model.AddressName;
        address.PostCode = model.PostCode;

        await _repository.SaveChangesAsync(cancellationToken);
    }
}