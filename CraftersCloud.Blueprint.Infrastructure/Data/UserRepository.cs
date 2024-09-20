using CraftersCloud.Blueprint.Domain.Users;
using CraftersCloud.Core.EntityFramework;
using CraftersCloud.Core.EntityFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Infrastructure.Data;

public class UserRepository(DbContext context) : EntityFrameworkRepository<User, Guid>(context);