using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Blueprint.Domain.Companies.DomainEvents;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Helpers;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Domain.Users.Commands;

[UsedImplicitly]
public class CreateOrUpdateUserCommandHandler
    (IRepository<User, Guid> userRepository, IRepository<Company, Guid> companyRepository
    /*IQueryable<Company> companies*/) : IRequestHandler<CreateOrUpdateUser.Command, User>
{
    public async Task<User> Handle(CreateOrUpdateUser.Command request,
        CancellationToken cancellationToken)
    {
        // todo if company by name does not exist, create it and assign it to the user
        if (request.CompanyName.HasContent())
        {
            //var company = companies.QueryByName(request.CompanyName).FirstOrDefault();
            //if (company == null)
            //{ 

            //}
            Company? company;
            company = await companyRepository.FindByIdAsync(request.CompanyId);
            if (company == null)
            {
                var newCompany = new Company { Name = request.CompanyName };
                newCompany.AddDomainEvent(new CompanyCreatedDomainEvent(request.CompanyName));
            }
        }
        User? user;
        if (request.Id.HasValue)
        {
            user = await userRepository.FindByIdAsync(request.Id.Value);
            if (user == null)
            {
                throw new InvalidOperationException("missing user");
            }
            user.Update(request);
        }
        else
        {            
            user = User.Create(request);
            userRepository.Add(user);
        }

        return user;
    }
}