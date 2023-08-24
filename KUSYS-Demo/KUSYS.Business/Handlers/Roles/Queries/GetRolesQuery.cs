using EasyMapper;
using KUSYS.Core.Models.Response;
using KUSYS.DataAccess.Repositories.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Business.Handlers.Roles.Queries
{
    public class GetRolesQuery : IRequest<List<RoleResponse>>
    {
        public class GetCourseStudentssQueryHandler : IRequestHandler<GetRolesQuery, List<RoleResponse>>
        {
            private readonly IRoleRepository _roleRepository;

            public GetCourseStudentssQueryHandler(IRoleRepository roleRepository)
            {
                _roleRepository = roleRepository;
            }

            public async Task<List<RoleResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
            {
                var roles = await _roleRepository.AsQueryable().ToListAsync(cancellationToken);
                return roles.ToMap<RoleResponse>().ToList();
            }
        }
    }
}
