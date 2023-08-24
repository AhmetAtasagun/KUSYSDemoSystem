using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using static KUSYS.Business.Handlers.Students.Commands.DeleteStudentCommand;

namespace KUSYS.Test
{
    public class DeleteStudentCommandTest
    {
        DeleteStudentCommand command;
        DeleteStudentCommandHandler commandHandler;
        Mock<IStudentRepository> studentRepositoryMock;

        [SetUp]
        public void Setup()
        {
            studentRepositoryMock = new Mock<IStudentRepository>();
            commandHandler = new DeleteStudentCommandHandler(studentRepositoryMock.Object);
        }

        [Test]
        public async Task UpdateStudentCommand_Success()
        {
            var student = GetStudent();
            command = new DeleteStudentCommand { StudentId = student.Id };
            studentRepositoryMock.Setup(s => s.FirstOrDefaultAsync(It.IsAny<Expression<Func<Student, bool>>>(), CancellationToken.None)).Returns(student);
            studentRepositoryMock.Setup(s => s.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
            var result = commandHandler.Handle(command, CancellationToken.None);
            Assert.True(result.Result);
        }

        private Task<Student> GetStudent()
        {
            var student = new Student { Id = 1, FirstName = "Ali", LastName = "Ata", BirthDate = DateTime.Now.AddYears(-30).AddDays(-65) };
            return Task.FromResult(student);
        }
    }
}