using EasyMapper;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using MediatR;

namespace KUSYS.Business.Handlers.Students.Commands
{
    public class UpdateStudentCommand : IRequest<StudentResponse>
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<int> Courses { get; set; }

        public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentResponse>
        {
            private readonly IStudentRepository _studentRepository;
            private readonly ICourseRepository _courseRepository;

            public UpdateStudentCommandHandler(IStudentRepository studentRepository, ICourseRepository courseRepository)
            {
                _studentRepository = studentRepository;
                _courseRepository = courseRepository;
            }

            public async Task<StudentResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
            {
                var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == request.StudentId, cancellationToken);
                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.BirthDate = request.BirthDate;
                student.Courses = _courseRepository.WhereNoTracking(w => request.Courses.Contains(w.Id)).ToList();
                _studentRepository.Update(student);
                await _studentRepository.SaveChangesAsync(cancellationToken);
                return student.ToMap<StudentResponse>();
            }
        }
    }
}
