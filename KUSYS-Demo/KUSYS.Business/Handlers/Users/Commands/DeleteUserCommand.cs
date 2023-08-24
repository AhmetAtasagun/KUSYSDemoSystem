using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;

namespace KUSYS.Business.Handlers.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
        {
            private readonly IUserRepository _userRepository;

            public DeleteUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var student = await _userRepository.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
                _userRepository.Delete(student);
                return await _userRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
