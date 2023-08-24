using FluentValidation;
using KUSYS.Business.Handlers.Users.Commands;
using KUSYS.DataAccess.Repositories.Abstracts;

namespace KUSYS.Business.Handlers.Users.ValidationRules
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Password).Equal(x => x.PasswordConfirm);
        }
    }

    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator(IUserRepository userRepository)
        {
            RuleFor(x => userRepository.Any(a => a.Id == x.UserId)).NotNull();
            RuleFor(x => x.Username).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Password).Equal(x => x.PasswordConfirm);
        }
    }
}
