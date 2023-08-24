using EasyMapper;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Business.Handlers.Students.Queries
{
    public class GetStudentsQuery : IRequest<List<StudentResponse>>
    {
        public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<StudentResponse>>
        {
            private readonly IStudentRepository _studentRepository;

            public GetStudentsQueryHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<List<StudentResponse>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
            {
                var students = _studentRepository.AsQueryable().ToList();
                var studentsModel = students.ToMap<StudentResponse>();
                return studentsModel.ToList();
            }
        }
    }
}
