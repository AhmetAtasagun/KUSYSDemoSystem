using KUSYS.Business.Handlers.Students.Queries;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using Moq;
using static KUSYS.Business.Handlers.Students.Queries.GetStudentsQuery;

namespace KUSYS.Test
{
    public class GetStudentsQueryTest
    {
        GetStudentsQuery query;
        GetStudentsQueryHandler queryHandler;
        Mock<IStudentRepository> studentRepositoryMock;

        [SetUp]
        public void Setup()
        {
            studentRepositoryMock = new Mock<IStudentRepository>();
            query = new GetStudentsQuery();
            queryHandler = new GetStudentsQueryHandler(studentRepositoryMock.Object);
        }

        [Test]
        public async Task GetStudentsQueryTest_Success()
        {
            var students = GetStudents();
            studentRepositoryMock.Setup(s => s.AsQueryable()).Returns(students.AsQueryable());
            var result = await queryHandler.Handle(query, CancellationToken.None);

            foreach (var item in students.Zip(result))
            {
                Assert.That(item.Second, Is.Not.Null);
                Assert.True(item.Second.FirstName == item.First.FirstName);
                Assert.True(item.Second.LastName == item.First.LastName);
                Assert.True(item.Second.BirthDate == item.First.BirthDate);
            }
        }

        private List<Student> GetStudents()
        {
            return new List<Student> {
                new Student { Id = 1, FirstName = "Ali", LastName = "Ata", BirthDate = DateTime.Now.AddYears(-30).AddDays(-65), Courses = new List<Course> { new Course { Name = "Matematik" }, new Course { Name = "Tarih" } } },
                new Student { Id = 2, FirstName = "Veli", LastName = "Ata", BirthDate = DateTime.Now.AddYears(-28).AddDays(-85), Courses = new List<Course> { new Course { Name = "Matematik" }, new Course { Name = "Tarih" } } },
                new Student { Id = 3, FirstName = "Kamil", LastName = "Ata", BirthDate = DateTime.Now.AddYears(-26).AddDays(-105), Courses = new List<Course> { new Course { Name = "Matematik" }, new Course { Name = "Tarih" } } },
            };
        }
    }
}