using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Api.Controllers
{
    public class RoleController : Controller
    {
        [HttpGet("")]
        public Task GetList()
        {
            return default;
        }
    }
}
