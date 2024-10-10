using CraftersCloud.Core.Data;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Domain.Categories.Commands;

public class CreateOrUpdateCategory
{
    [PublicAPI]
    public class Command : IRequest<Category>
    {
        public required Guid? Id { get; set; }
        public required string Name { get; set; }
    }

    [UsedImplicitly]
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IRepository<Category> categoryRepository) =>
            RuleFor(x => x.Name).NotEmpty().MaximumLength(Category.NameMaxLength);
    }
}
