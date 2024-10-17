using CraftersCloud.Blueprint.Domain;
using CraftersCloud.Blueprint.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftersCloud.Blueprint.Infrastructure.Data.Configurations;

public class UserCompanyHistoryConfiguration : IEntityTypeConfiguration<UserCompanyHistory>
{
    public void Configure(EntityTypeBuilder<UserCompanyHistory> builder)
    {
        builder.HasKey(c => new { c.CompanyId, c.UserId });
    }
}