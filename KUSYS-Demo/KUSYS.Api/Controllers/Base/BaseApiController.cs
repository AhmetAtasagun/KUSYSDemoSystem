using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Api.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
