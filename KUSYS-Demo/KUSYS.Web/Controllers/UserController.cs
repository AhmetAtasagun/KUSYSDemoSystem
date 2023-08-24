using KUSYS.Business.Handlers.Users.Commands;
using KUSYS.Business.Handlers.Users.Queries;
using KUSYS.Business.Infrastructure;
using KUSYS.Core.Infrastructure;
using KUSYS.Core.Models.Response;
using KUSYS.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Web.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [Auth(Roles = KeyConsts.Admin)]
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await Mediator.Send(new GetUsersQuery(), cancellationToken));
        }

        [Auth(Roles = KeyConsts.Admin)]
        [HttpGet]
        public async Task<UserResponse> Get(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetUserQuery { UserId = id }, cancellationToken);
        }

        [Auth(Roles = KeyConsts.Admin)]
        [HttpPost]
        public async Task<UserResponse> Add(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [Auth(Roles = KeyConsts.Admin)]
        [HttpPut]
        public async Task<UserResponse> Update(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [Auth(Roles = KeyConsts.Admin)]
        [HttpDelete]
        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new DeleteUserCommand { UserId = id }, cancellationToken);
        }
    }
}
