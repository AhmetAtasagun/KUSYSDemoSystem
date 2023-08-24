using FluentValidation;
using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.DataAccess.Repositories.Abstracts;

namespace KUSYS.Business.Handlers.Students.ValidationRules
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.BirthDate).NotEmpty();
        }
    }

    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentValidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.StudentId).GreaterThan(0);
            RuleFor(x => studentRepository.Any(a => a.Id == x.StudentId)).NotNull();
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.BirthDate).NotEmpty();
        }
    }
}
