using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Request;

namespace OpenStore.Omnichannel.Identity.Services
{
    public interface IUserService : ICrudService<ApplicationUser, ApplicationUserDto>
    {
        Task AddToRole(Guid userId, string role, CancellationToken cancellationToken = default);
        Task RemoveFromRole(Guid userId, string role, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> GetUserRoles(Guid id, CancellationToken cancellationToken = default);
        Task RevokeUserToken(Guid userId, CancellationToken cancellationToken = default);
        Task ChangePassword(Guid userId, ChangePasswordRequest model, CancellationToken cancellationToken);
    }
}