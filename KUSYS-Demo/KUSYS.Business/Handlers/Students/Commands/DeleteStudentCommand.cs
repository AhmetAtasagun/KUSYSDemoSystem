using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;

namespace KUSYS.Business.Handlers.Students.Commands
{
    public class DeleteStudentCommand : IRequest<bool>
    {
        public int StudentId { get; set; }
        public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
        {
            private readonly IStudentRepository _studentRepository;

            public DeleteStudentCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
            {
                var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == request.StudentId, cancellationToken);
                _studentRepository.Delete(student);
                return await _studentRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
