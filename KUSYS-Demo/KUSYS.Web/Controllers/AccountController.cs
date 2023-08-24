using KUSYS.Business.Handlers.Auth.Commands;
using KUSYS.Business.Handlers.Users.Commands;
using KUSYS.Core.Infrastructure;
using KUSYS.Web.Controllers.Base;
using KUSYS.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Web.Controllers
{
    public class AccountController : BaseController
    {
        internal readonly static string TempKey = "loginMessage";
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            if (result == null)
                TempData[TempKey] = MessageConsts.UserPreviouslyRegistered;
            else
                TempData[TempKey] = MessageConsts.RecordSuccess;
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromForm] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                TempData[TempKey] = MessageConsts.UsernameOrPasswordWrong;
                return RedirectToAction("Login");
            }
            HttpContext.SetAuthUser(result);
            return RedirectToAction("Index", "Home");
        }

        [Auth(Roles = KeyConsts.Admin + ", " + KeyConsts.User)]
        public IActionResult Logout()
        {
            HttpContext.DeleteAuthUser();
            return RedirectToAction("Login");
        }
    }
}
