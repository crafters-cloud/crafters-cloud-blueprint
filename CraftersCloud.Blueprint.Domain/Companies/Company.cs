using CraftersCloud.Blueprint.Core.Entities;
using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Blueprint.Domain.Companies.DomainEvents;

namespace CraftersCloud.Blueprint.Domain.Companies;

public class Company : EntityWithCreatedUpdated
{
    public const int NameMaxLength = 200;
    public static readonly Guid? SystemCompanyId = new("741656C1-0234-44FC-81A1-37DE4314D624");
    public string Name { get; private set; } = string.Empty;

    public static Company Create(CreateOrUpdateCompany.Command command)
    {
        var result = new Company {Name = command.Name};
        result.AddDomainEvent(new CompanyCreatedDomainEvent(result.Name));

        return result;
    }

    public void Update(CreateOrUpdateCompany.Command command)
    {
        Name = command.Name;
        AddDomainEvent(new CompanyUpdatedDomainEvent(Name));
    }
}
