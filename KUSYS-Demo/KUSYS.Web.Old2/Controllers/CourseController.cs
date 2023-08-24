using KUSYS.Business.Handlers.Courses.Queries;
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

        [HttpGet]
        public async Task<List<CourseStudentsResponse>> GetList(CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetCourseStudentsQuery(), cancellationToken);
        }
    }
}
