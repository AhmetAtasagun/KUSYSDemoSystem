using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.DataAccess.Repositories;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using static KUSYS.Business.Handlers.Students.Commands.UpdateStudentCommand;

namespace KUSYS.Test
{
    public class UpdateStudentCommandTest
    {
        UpdateStudentCommand command;
        UpdateStudentCommandHandler commandHandler;
        Mock<IStudentRepository> studentRepositoryMock;
        Mock<ICourseRepository> courseRepositoryMock;

        [SetUp]
        public void Setup()
        {
            studentRepositoryMock = new Mock<IStudentRepository>();
            courseRepositoryMock = new Mock<ICourseRepository>();
            commandHandler = new UpdateStudentCommandHandler(studentRepositoryMock.Object, courseRepositoryMock.Object);
        }

        [Test]
        public async Task UpdateStudentCommand_Success()
        {
            var student = GetStudent();
            command = new UpdateStudentCommand
            {
                StudentId = student.Result.Id,
                FirstName = "Akif",
                LastName = "Ata",
                BirthDate = DateTime.Now.AddYears(-30).AddDays(-85),
            };
            studentRepositoryMock.Setup(s => s.FirstOrDefaultAsync(It.IsAny<Expression<Func<Student, bool>>>(), CancellationToken.None)).Returns(student);
            courseRepositoryMock.Setup(s => s.WhereNoTracking(It.IsAny<Expression<Func<Course, bool>>>())).Returns(GetCourses());

            var result = await commandHandler.Handle(command, CancellationToken.None);
            Assert.True(result.FirstName == command.FirstName);
            Assert.True(result.BirthDate == command.BirthDate);
        }

        private IQueryable<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course { Name = "Matematik" },
                new Course { Name = "Tarih" }
            }.AsQueryable();
        }

        private Task<Student> GetStudent()
        {
            var student = new Student { Id = 1, FirstName = "Ali", LastName = "Ata", BirthDate = DateTime.Now.AddYears(-30).AddDays(-65) };
            return Task.FromResult(student);
        }
    }
}