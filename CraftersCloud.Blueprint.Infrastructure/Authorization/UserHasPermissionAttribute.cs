using CraftersCloud.Blueprint.Domain.Authorization;
using Enigmatry.Entry.AspNetCore.Authorization.Attributes;

namespace CraftersCloud.Blueprint.Infrastructure.Authorization;

public sealed class UserHasPermissionAttribute(params PermissionId[] permissions) : UserHasPermissionAttribute<PermissionId>(permissions);