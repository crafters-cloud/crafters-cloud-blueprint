using CraftersCloud.Blueprint.Domain.Authorization;

namespace CraftersCloud.Blueprint.Domain.Identity;

public sealed class PermissionsContext(IReadOnlyCollection<PermissionId> values)
{
    private IReadOnlyCollection<PermissionId> Values { get; } = values;
    public static PermissionsContext Empty { get; } = new([]);

    public bool Contains(PermissionId permissionId) => Values.Contains(permissionId);

    public bool ContainsAny(IEnumerable<PermissionId> permissionIds) => Values.Intersect(permissionIds).Any();
}