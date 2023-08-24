using KUSYS.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Api.Controllers
{
    public class UserController : BaseApiController
    {

        [HttpGet("")]
        public Task GetList()
        {
            return default;
        }
    }
}
