using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Core.Data;
using CraftersCloud.Core.Entities;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace CraftersCloud.Blueprint.Domain.Users.Commands;

public static class CreateOrUpdateUser
{
    [PublicAPI]
    public class Command : IRequest<User>
    {
        public required Guid? Id { get; set; }
        public required string EmailAddress { get; set; } = string.Empty;
        public required string FullName { get; set; } = string.Empty;
        public required Guid RoleId { get; set; }
        public UserStatusId UserStatusId { get; set; } = UserStatusId.Active;
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
    }

    [UsedImplicitly]
    public class Validator : AbstractValidator<Command>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Company> _companyRepository;

        public Validator(IRepository<User> userRepository, IRepository<Company> companyRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;

            RuleFor(x => x.CompanyName).MaximumLength(Company.NameMaxLength);
            RuleFor(x => x.EmailAddress).NotEmpty().MaximumLength(User.EmailAddressMaxLength).EmailAddress();
            RuleFor(x => x.EmailAddress).Must(UniqueEmailAddress).WithMessage("EmailAddress is already taken");
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(User.NameMaxLength);
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.CompanyId).Must(CompanyWithIdMustExist).WithMessage("Company not found");
        }

        private bool CompanyWithIdMustExist(Guid? companyId) =>
            !companyId.HasValue || _companyRepository.QueryAll()
                .QueryById(companyId.Value).Any();

        private bool UniqueEmailAddress(Command command, string name) =>
            !_userRepository.QueryAll()
                .QueryExceptWithId(command.Id)
                .QueryByEmailAddress(name)
                .Any();
    }
}