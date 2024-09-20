using CraftersCloud.Blueprint.Domain.Authorization;

namespace CraftersCloud.Blueprint.Infrastructure.Data.Configurations;

#pragma warning disable CA1711 // Identifiers should not have incorrect suffix

// Helper class used for easier data configuration and data seeding
public class RolePermission
{
    public Guid RoleId { get; init; }
    public PermissionId PermissionId { get; init; }
}