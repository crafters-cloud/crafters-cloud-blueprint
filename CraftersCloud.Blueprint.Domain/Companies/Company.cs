using CraftersCloud.Blueprint.Core.Entities;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Blueprint.Domain.Companies.DomainEvents;
using CraftersCloud.Blueprint.Domain.Users;

namespace CraftersCloud.Blueprint.Domain.Companies;

public class Company : EntityWithCreatedUpdated
{
    public const int NameMaxLength = 200;
    public string Name { get; private set; } = string.Empty;
    private readonly IList<User> _users = [];
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();
    private readonly IList<UserCompanyHistory> _userCompanyHistories = [];
    public IReadOnlyCollection<UserCompanyHistory> UserCompanyHistories => _userCompanyHistories.AsReadOnly();
    public static Company Create(CreateOrUpdateCompany.Command command) => Create(command.Name);

    public static Company Create(string companyName)
    {
        var result = new Company { Name = companyName };
        result.AddDomainEvent(new CompanyCreatedDomainEvent(result.Name));

        return result;
    }

    public void Update(CreateOrUpdateCompany.Command command)
    {
        Name = command.Name;
        AddDomainEvent(new CompanyUpdatedDomainEvent(Name));
    }
}
