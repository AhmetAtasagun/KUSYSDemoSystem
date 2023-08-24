using KUSYS.Business.Handlers.Students.Queries;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using static KUSYS.Business.Handlers.Students.Queries.GetStudentQuery;

namespace KUSYS.Test
{
    public class GetStudentQueryTest
    {
        GetStudentQuery query;
        GetStudentQueryHandler queryHandler;
        Mock<IStudentRepository> studentRepositoryMock;

        [SetUp]
        public void Setup()
        {
            studentRepositoryMock = new Mock<IStudentRepository>();
            query = new GetStudentQuery();
            queryHandler = new GetStudentQueryHandler(studentRepositoryMock.Object);
        }

        [Test]
        public async Task GetStudentsQueryTest_Success()
        {
            var student = GetStudent();
            studentRepositoryMock.Setup(s => s.FirstOrDefaultNoTrackingAsync(It.IsAny<Expression<Func<Student, bool>>>(), CancellationToken.None)).Returns(student);
            var result = await queryHandler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.True(result.FirstName == student.Result.FirstName);
            Assert.True(result.LastName == student.Result.LastName);
            Assert.True(result.BirthDate == student.Result.BirthDate);
        }

        private Task<Student> GetStudent()
        {
            var student = new Student { Id = 1, FirstName = "Ali", LastName = "Ata", BirthDate = DateTime.Now.AddYears(-30).AddDays(-65) };
            return Task.FromResult(student);
        }
    }
}