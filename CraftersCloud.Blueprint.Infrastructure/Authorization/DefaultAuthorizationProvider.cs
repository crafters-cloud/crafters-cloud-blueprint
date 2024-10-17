using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Identity;
using CraftersCloud.Core.AspNetCore.Authorization;

namespace CraftersCloud.Blueprint.Infrastructure.Authorization;

public class DefaultAuthorizationProvider(ICurrentUserProvider currentUserProvider)
    : IAuthorizationProvider<PermissionId>
{
    public bool AuthorizePermissions(IEnumerable<PermissionId> permissions) =>
        currentUserProvider.User?.Permissions.ContainsAny(permissions) ?? false;
}