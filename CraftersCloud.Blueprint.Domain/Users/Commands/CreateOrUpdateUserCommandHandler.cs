using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Companies.Commands;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Entities;
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
        if (request.CompanyName.HasContent())
        {
            var company = await companyRepository.QueryAll().QueryByNameExact(request.CompanyName).SingleOrDefaultAsync(cancellationToken);
            if (company == null)
            {
                var command = new CreateOrUpdateCompany.Command
                {
                    Id = SequentialGuidGenerator.Generate(),
                    Name = request.CompanyName
                };
                company = Company.Create(command);
                request.CompanyId = company.Id;
                companyRepository.Add(company);
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