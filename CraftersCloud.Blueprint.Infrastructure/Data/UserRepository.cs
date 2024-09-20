using CraftersCloud.Blueprint.Domain.Users;
using Enigmatry.Entry.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Infrastructure.Data;

public class UserRepository(DbContext context) : EntityFrameworkRepository<User, Guid>(context);