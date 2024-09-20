using CraftersCloud.Blueprint.Domain.Users;
using JetBrains.Annotations;

namespace CraftersCloud.Blueprint.Infrastructure.Data.Configurations;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class UserStatusConfiguration : EntityWithEnumIdConfiguration<UserStatus, UserStatusId>;