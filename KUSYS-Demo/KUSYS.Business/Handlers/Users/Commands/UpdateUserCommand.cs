using EasyMapper;
using KUSYS.Business.Helpers;
using KUSYS.Core.Models.Authorization;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;

namespace KUSYS.Business.Handlers.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserResponse>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public int RoleId { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
        {
            private readonly IUserRepository _userRepository;

            public UpdateUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

                if (!HashingHelper.CheckPassword(user.ToMap<UserAuth>(), request.Password))
                    user.Password = HashingHelper.HashUse(user.Password, user.PasswordSalt);
                user.Username = request.Username;
                user.RoleId = request.RoleId;

                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync(cancellationToken);
                return user.ToMap<UserResponse>();
            }
        }
    }
}
