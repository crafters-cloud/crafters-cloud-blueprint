using CraftersCloud.Core.Data;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Domain.Companies.Commands
{
    public static class CreateOrUpdateCompany
    {
        [PublicAPI]
        public class Command : IRequest<Company>
        {
            public required Guid? Id { get; set; }
            public required string Name { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            private readonly IRepository<Company> _companyRepository;

            public Validator(IRepository<Company> companyRepository)
            {
                _companyRepository = companyRepository;

                RuleFor(x => x.Name).NotEmpty().MaximumLength(Company.NameMaxLength);
            }
        }
    }
}
