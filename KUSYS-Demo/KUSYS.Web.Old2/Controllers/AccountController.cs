using KUSYS.Business.Handlers.Auth.Commands;
using KUSYS.Business.Handlers.Users.Commands;
using KUSYS.Web.Controllers.Base;
using KUSYS.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromForm] LoginCommand command)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromForm] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            HttpContext.SetAuthUser(result);
            return RedirectToAction("Index", "Student");
        }

        public IActionResult Logout()
        {
            HttpContext.DeleteAuthUser();
            return RedirectToAction("Login");
        }
    }
}
