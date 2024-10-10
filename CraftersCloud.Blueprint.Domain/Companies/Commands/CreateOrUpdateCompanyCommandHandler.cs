using CraftersCloud.Core.Data;
using CraftersCloud.Core.Helpers;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Domain.Companies.Commands;

[UsedImplicitly]
public class CreateOrUpdateCompanyCommandHandler : IRequestHandler<CreateOrUpdateCompany.Command, Company>
{
    private readonly IRepository<Company, Guid> _companyRepository;

    public CreateOrUpdateCompanyCommandHandler(IRepository<Company, Guid> companyRepository) => _companyRepository = companyRepository;

    public async Task<Company> Handle(CreateOrUpdateCompany.Command request, CancellationToken cancellationToken)
    {
        Company? company;
        if (request.Id.HasValue)
        {
            company = await _companyRepository.FindByIdAsync(request.Id.Value);
            if (company == null) throw new InvalidOperationException("missing company");
            company.Update(request);
        }
        else
        {
            company = Company.Create(request);
            _companyRepository.Add(company);
        }

        return company;
    }
}
