using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Users;

namespace CraftersCloud.Blueprint.Domain;

public class UserCompanyHistories
{
    public Guid? UserId { get; set; }
    public Guid? CompanyId { get; set; }
    public User User { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public DateOnly EnrollmentDate { get; set; }
    public DateTimeOffset EnrollmentDateTime { get; set; }
}
