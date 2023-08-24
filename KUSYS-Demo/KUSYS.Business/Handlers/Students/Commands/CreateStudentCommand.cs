using EasyMapper;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using MediatR;

namespace KUSYS.Business.Handlers.Students.Commands
{
    public class CreateStudentCommand : IRequest<StudentResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<int> Courses { get; set; }

        public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentResponse>
        {
            private readonly IStudentRepository _studentRepository;
            private readonly ICourseRepository _courseRepository;

            public CreateStudentCommandHandler(IStudentRepository studentRepository, ICourseRepository courseRepository)
            {
                _studentRepository = studentRepository;
                _courseRepository = courseRepository;
            }

            public async Task<StudentResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
            {
                var courses = _courseRepository.WhereNoTracking(w => request.Courses.Contains(w.Id)).ToList();
                var student = new Student
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    Courses = courses
                };
                _studentRepository.Add(student);
                await _studentRepository.SaveChangesAsync(cancellationToken);
                return student.ToMap<StudentResponse>();
            }
        }
    }
}
