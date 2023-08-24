using EasyMapper;
using KUSYS.Business.Helpers;
using KUSYS.Business.Infrastructure;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.Domain.Entities;
using MediatR;

namespace KUSYS.Business.Handlers.Users.Commands
{
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public int RoleId { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IRoleRepository _roleRepository;

            public CreateUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
            {
                _userRepository = userRepository;
                _roleRepository = roleRepository;
            }

            public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var existsUser = _userRepository.Any(x => x.Username == request.Username);
                if (existsUser)
                {
                    return default;
                }
                var user = request.ToMap<User>();
                var salt = HashingHelper.GenerateSecurityCode();
                user.Password = HashingHelper.HashUse(user.Password, salt);
                user.PasswordSalt = salt;
                user.Role = _roleRepository.FirstOrDefaultNoTracking(w => w.Name == KeyConsts.User);
                _userRepository.Add(user);
                _userRepository.SaveChanges();
                return user.ToMap<UserResponse>();
            }
        }
    }
}
