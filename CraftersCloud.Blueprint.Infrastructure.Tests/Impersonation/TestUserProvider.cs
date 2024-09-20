using CraftersCloud.Blueprint.Domain.Users;
using CraftersCloud.Blueprint.Infrastructure.Identity;
using CraftersCloud.Core.Data;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace CraftersCloud.Blueprint.Infrastructure.Tests.Impersonation;

[UsedImplicitly]
public class TestUserProvider(IRepository<User> userRepository, ILogger<TestUserProvider> logger)
    : SystemUserProvider(userRepository, logger)
{
    public override Guid? UserId => TestUserData.TestUserId;
}