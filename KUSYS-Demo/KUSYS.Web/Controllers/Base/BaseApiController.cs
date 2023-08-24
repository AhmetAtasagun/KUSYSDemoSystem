using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IMediator Mediator;

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
