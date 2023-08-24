using EasyMapper;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Business.Handlers.Users.Queries
{
    public class GetUsersQuery : IRequest<List<UserResponse>>
    {
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
        {
            private readonly IUserRepository _userRepository;

            public GetUsersQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.Include(i => i.Role).ToListAsync(cancellationToken);
                var usersModel = users.ToMap<UserResponse>();
                return usersModel.ToList();
            }
        }
    }
}
