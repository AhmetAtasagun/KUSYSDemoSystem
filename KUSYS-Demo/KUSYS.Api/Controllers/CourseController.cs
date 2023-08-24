using KUSYS.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Api.Controllers
{
    public class CourseController : BaseApiController
    {

        [HttpGet("")]
        public Task GetList()
        {
            return default;
        }
    }
}
