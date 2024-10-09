using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Entities;
using CraftersCloud.Core.EntityFramework;
using CraftersCloud.Core.Helpers;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Domain.Users.Commands;

[UsedImplicitly]
public class CreateOrUpdateUserCommandHandler
    (IRepository<User, Guid> userRepository, IRepository<Company, Guid> companyRepository) 
    : IRequestHandler<CreateOrUpdateUser.Command, User>
{
    public async Task<User> Handle(CreateOrUpdateUser.Command request,
        CancellationToken cancellationToken)
    {
        if (request.CompanyName.HasContent())
        {
            var company = await companyRepository.QueryAll()
                .QueryByNameExact(request.CompanyName)
                .SingleOrDefaultAsync(cancellationToken);

            if (company == null)
            {
                company = Company.Create(request.CompanyName);
                request.CompanyId = company.Id;
                companyRepository.Add(company);
            }
        }

        User? user;
        if (request.Id.HasValue)
        {
            user = await userRepository.QueryAll()
                .QueryById(request.Id.Value)
                .SingleOrNotFoundAsync(cancellationToken: cancellationToken);
            
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