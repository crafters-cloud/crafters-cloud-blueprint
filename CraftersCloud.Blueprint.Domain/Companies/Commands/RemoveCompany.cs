using CraftersCloud.Blueprint.Domain.Products;
using CraftersCloud.Blueprint.Domain.Users;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Domain.Companies.Commands;

public static class RemoveCompany
{
    [PublicAPI]
    public class Command : IRequest
    {
        public Guid Id { get; set; }
    }

    [UsedImplicitly]
    public class RequestHandler(IRepository<Company> companyRepository, IRepository<User> userRepository) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var company = await companyRepository.QueryAll().QueryById(request.Id).SingleOrNotFoundAsync(cancellationToken);
            List<User> users = userRepository.QueryAll().QueryByCompanyId(company.Id).ToList();
            
            if (users.Count != 0) throw new InvalidOperationException($"Company has users: {users.Count}");
            if (company == null) throw new InvalidOperationException("No such company: Invalid ID");
            
            companyRepository.Delete(company);
        }
    }
}