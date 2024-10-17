using CraftersCloud.Core.Data;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Domain.Companies.Commands;

[UsedImplicitly]
public class CreateOrUpdateCompanyCommandHandler(IRepository<Company, Guid> companyRepository)
    : IRequestHandler<CreateOrUpdateCompany.Command, Company>
{
    public async Task<Company> Handle(CreateOrUpdateCompany.Command request, CancellationToken cancellationToken)
    {
        Company? company;
        if (request.Id.HasValue)
        {
            company = await companyRepository.FindByIdAsync(request.Id.Value);
            if (company == null) throw new InvalidOperationException("missing company");
            company.Update(request);
        }
        else
        {
            company = Company.Create(request);
            companyRepository.Add(company);
        }

        return company;
    }
}
