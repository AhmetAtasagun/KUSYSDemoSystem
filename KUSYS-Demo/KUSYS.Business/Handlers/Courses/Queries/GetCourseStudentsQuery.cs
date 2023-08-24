using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Business.Handlers.Courses.Queries
{
    public class GetCourseStudentsQuery : IRequest<List<CourseStudentsResponse>>
    {
        public int StudentId { get; set; }
        public class GetCourseStudentssQueryHandler : IRequestHandler<GetCourseStudentsQuery, List<CourseStudentsResponse>>
        {
            private readonly ICourseRepository _courseRepository;

            public GetCourseStudentssQueryHandler(ICourseRepository courseRepository)
            {
                _courseRepository = courseRepository;
            }

            public async Task<List<CourseStudentsResponse>> Handle(GetCourseStudentsQuery request, CancellationToken cancellationToken)
            {
                var courseQuery = _courseRepository.Include(i => i.Students).AsQueryable();
                if (request.StudentId > 0)
                    courseQuery = courseQuery.Where(w => w.Students.Any(a => a.Id == request.StudentId));
                var courses = await courseQuery
                    .Select(s => new CourseStudentsResponse
                    {
                        Id = s.Id,
                        CourseName = s.Name,
                        Students = s.Students.Select(ss => new StudentResponse
                        {
                            FirstName = ss.FirstName,
                            LastName = ss.LastName,
                            BirthDate = ss.BirthDate
                        })
                    }).ToListAsync(cancellationToken);
                return courses;
            }
        }
    }
}
