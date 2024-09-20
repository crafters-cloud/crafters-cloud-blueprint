using CraftersCloud.Blueprint.Core.Entities;
using CraftersCloud.Blueprint.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftersCloud.Blueprint.Infrastructure.Data.Configurations;

public static class EntityTypeBuilderExtensions
{
    public static void HasCreatedByAndUpdatedBy<T>(this EntityTypeBuilder<T> builder) where T : EntityWithCreatedUpdated
    {
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.CreatedById).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UpdatedById).OnDelete(DeleteBehavior.NoAction);
    }
}