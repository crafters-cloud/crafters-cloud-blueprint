using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Helpers;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Domain.Users.Commands;

[UsedImplicitly]
public class CreateOrUpdateUserCommandHandler
    (IRepository<User, Guid> userRepository, IRepository<Company, Guid> companyRepository) : IRequestHandler<CreateOrUpdateUser.Command, User>
{
    public async Task<User> Handle(CreateOrUpdateUser.Command request,
        CancellationToken cancellationToken)
    {
        // todo if company by name does not exist, create it and assign it to the user
        //var company = await companyRepository.QueryAll().QueryById(request.CompanyId.GetValueOrDefault()).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        var company = await companyRepository.QueryAll().QueryByNameExact(request.CompanyName).SingleOrDefaultAsync(cancellationToken);
        if (request.CompanyName.HasContent() && company == null)
        {
            var command = new CreateOrUpdateCompany.Command
            {
                Id = request.CompanyId,
                Name = request.CompanyName
            };
            company = Company.Create(command);
            companyRepository.Add(company);
        }

        //todo: if the request has name of existing company, but no company id
        
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