using EasyMapper;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;

namespace KUSYS.Business.Handlers.Students.Queries
{
    public class GetStudentQuery : IRequest<StudentResponse>
    {
        public int StudentId { get; set; }
        public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentResponse>
        {
            private readonly IStudentRepository _studentRepository;

            public GetStudentQueryHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<StudentResponse> Handle(GetStudentQuery request, CancellationToken cancellationToken)
            {
                var student = await _studentRepository.FirstOrDefaultNoTrackingAsync(x => x.Id == request.StudentId, cancellationToken);
                var studentModel = student.ToMap<StudentResponse>();
                return studentModel;
            }
        }
    }
}
