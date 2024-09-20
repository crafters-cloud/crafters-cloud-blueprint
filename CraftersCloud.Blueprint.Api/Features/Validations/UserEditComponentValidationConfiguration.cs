using CraftersCloud.Blueprint.Api.Features.Users;
using CraftersCloud.Blueprint.Domain.Users;
using Enigmatry.Entry.CodeGeneration.Validation;

namespace CraftersCloud.Blueprint.Api.Features.Validations;

public class UserEditComponentValidationConfiguration : ValidationConfiguration<GetUserDetails.Response>
{
    public UserEditComponentValidationConfiguration() =>
        RuleFor(x => x.FullName)
            .IsRequired()
            .MaxLength(User.NameMaxLength);
}