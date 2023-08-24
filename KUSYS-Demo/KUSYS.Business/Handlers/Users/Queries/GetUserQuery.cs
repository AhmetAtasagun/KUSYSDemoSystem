using EasyMapper;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;

namespace KUSYS.Business.Handlers.Users.Queries
{
    public class GetUserQuery : IRequest<UserResponse>
    {
        public int UserId { get; set; }
        public class GetUsersQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
        {
            private readonly IUserRepository _userRepository;

            public GetUsersQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.FirstOrDefaultNoTrackingAsync(x => x.Id == request.UserId, cancellationToken);
                var usersModel = user.ToMap<UserResponse>();
                return usersModel;
            }
        }
    }
}
