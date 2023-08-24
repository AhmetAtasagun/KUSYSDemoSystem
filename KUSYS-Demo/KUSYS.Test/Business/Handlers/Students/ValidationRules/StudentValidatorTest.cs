using FluentValidation.TestHelper;
using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.Business.Handlers.Students.ValidationRules;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace KUSYS.Test
{
    public class StudentValidatorTest
    {
        CreateStudentValidator createStudentValidator;
        UpdateStudentValidator updateStudentValidator;
        Mock<IStudentRepository> studentRepositoryMock;

        [SetUp]
        public void Setup()
        {
            studentRepositoryMock = new Mock<IStudentRepository>();
            createStudentValidator = new CreateStudentValidator();
            updateStudentValidator = new UpdateStudentValidator(studentRepositoryMock.Object);
        }

        [Test]
        public async Task CreateStudentValidator_Success()
        {
            var command = new CreateStudentCommand { FirstName = "Ali", LastName = "Ata", BirthDate = new DateTime(1998, 5, 25) };
            var result = await createStudentValidator.TestValidateAsync(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public async Task CreateStudentValidator_FirstNameError()
        {
            var command = new CreateStudentCommand { FirstName = "K", LastName = "Alem", BirthDate = new DateTime(2018, 5, 25) };
            var result = await createStudentValidator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(e => e.FirstName);
        }

        [Test]
        public async Task CreateStudentValidator_LastNameError()
        {
            var command = new CreateStudentCommand { FirstName = "Ahmet", LastName = "A", BirthDate = new DateTime(1898, 5, 25) };
            var result = await createStudentValidator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(e => e.LastName);
        }








        [Test]
        public async Task UpdateStudentValidator_Success()
        {
            var command = new UpdateStudentCommand { StudentId = 1, FirstName = "Ali", LastName = "Ata", BirthDate = new DateTime(1998, 5, 25) };
            studentRepositoryMock.Setup(s => s.Any(It.IsAny<Expression<Func<Student, bool>>>())).Returns(true);
            var result = await updateStudentValidator.TestValidateAsync(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public async Task UpdateStudentValidator_UserNotExistsError()
        {
            var command = new UpdateStudentCommand { StudentId = 1, FirstName = "Ali", LastName = "Ata", BirthDate = new DateTime(1998, 5, 25) };
            studentRepositoryMock.Setup(s => s.Any(It.IsAny<Expression<Func<Student, bool>>>())).Returns(false);
            var result = await updateStudentValidator.TestValidateAsync(command);
            result.ShouldNotHaveValidationErrorFor("user");
        }

        [Test]
        public async Task UpdateStudentValidator_FirstNameError()
        {
            var command = new UpdateStudentCommand { StudentId = 1, FirstName = "K", LastName = "Alem", BirthDate = new DateTime(2018, 5, 25) };
            var result = await updateStudentValidator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(e => e.FirstName);
        }

        [Test]
        public async Task UpdateStudentValidator_LastNameError()
        {
            var command = new UpdateStudentCommand { FirstName = "Ahmet", LastName = "A", BirthDate = new DateTime(1898, 5, 25) };
            var result = await updateStudentValidator.TestValidateAsync(command);
            result.ShouldHaveValidationErrorFor(e => e.LastName);
        }
    }
}