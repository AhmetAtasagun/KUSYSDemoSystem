using KUSYS.Business.Handlers.Courses.Queries;
using KUSYS.Business.Infrastructure;
using KUSYS.Core.Infrastructure;
using KUSYS.Core.Models.Response;
using KUSYS.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Web.Controllers
{
    public class CourseController : BaseController
    {
        public CourseController(IMediator mediator) : base(mediator)
        {
        }

        [Auth(Roles = KeyConsts.Admin + ", " + KeyConsts.User)]
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await Mediator.Send(new GetCourseStudentsQuery(), cancellationToken));
        }

        [Auth(Roles = KeyConsts.Admin + ", " + KeyConsts.User)]
        [HttpGet]
        public async Task<List<CourseStudentsResponse>> GetList(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetCourseStudentsQuery { StudentId = id }, cancellationToken);
        }

        //[Auth(Roles = KeyConsts.Admin + ", " + KeyConsts.User)]
        //[HttpGet]
        //public async Task<List<CourseStudentsResponse>> GetByStudentId(int id, CancellationToken cancellationToken)
        //{
        //    return await Mediator.Send(new GetCourseStudentsQuery(), cancellationToken);
        //}
    }
}
