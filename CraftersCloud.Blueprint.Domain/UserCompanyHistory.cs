using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Users;

namespace CraftersCloud.Blueprint.Domain;

public class UserCompanyHistory
{
    public Guid? UserId { get; init; }
    public Guid? CompanyId { get; init; }
    public User User { get; init; } = null!;
    public Company Company { get; init; } = null!;
    public DateOnly EnrollmentDate { get; init; }
    public DateTimeOffset EnrollmentDateTime { get; init; }
}
