using EasyMapper;
using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.Core.Abstracts;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using static KUSYS.Business.Handlers.Students.Commands.CreateStudentCommand;

namespace KUSYS.Test.Business.Handlers.Students.Commands
{
    public class CreateStudentCommandTest
    {
        CreateStudentCommand command;
        CreateStudentCommandHandler commandHandler;
        Mock<IStudentRepository> studentRepositoryMock;
        Mock<ICourseRepository> courseRepositoryMock;

        [SetUp]
        public void Setup()
        {
            studentRepositoryMock = new Mock<IStudentRepository>();
            courseRepositoryMock = new Mock<ICourseRepository>();
            commandHandler = new CreateStudentCommandHandler(studentRepositoryMock.Object, courseRepositoryMock.Object);
        }

        [Test]
        public async Task CreateStudentCommand_Success()
        {
            command = new CreateStudentCommand
            {
                FirstName = "Akif",
                LastName = "Ata",
                BirthDate = DateTime.Now.AddYears(-30).AddDays(-85),
            };
            courseRepositoryMock.Setup(s => s.WhereNoTracking(It.IsAny<Expression<Func<Course, bool>>>())).Returns(GetCourses());
            var student = command.ToMap<Student>();

            var result = await commandHandler.Handle(command, CancellationToken.None);
            Assert.True(result.FirstName == student.FirstName);
            Assert.True(result.BirthDate == student.BirthDate);
        }

        private IQueryable<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course { Name = "Matematik" },
                new Course { Name = "Tarih" }
            }.AsQueryable();
        }
    }
}
