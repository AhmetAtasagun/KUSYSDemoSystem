using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.Business.Handlers.Students.Queries;
using KUSYS.Business.Infrastructure;
using KUSYS.Core.Infrastructure;
using KUSYS.Core.Models.Response;
using KUSYS.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Web.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(IMediator mediator) : base(mediator)
        {
        }

        [Auth(Roles = KeyConsts.Admin + ", " + KeyConsts.User)]
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await Mediator.Send(new GetStudentsQuery(), cancellationToken));
        }

        [Auth(Roles = KeyConsts.Admin + ", " + KeyConsts.User)]
        [HttpGet]
        public async Task<StudentResponse> Get(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetStudentQuery { StudentId = id }, cancellationToken);
        }

        [Auth(Roles = KeyConsts.Admin)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return RedirectToAction("Index");
        }

        [Auth(Roles = KeyConsts.Admin)]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return RedirectToAction("Index");
        }

        [Auth(Roles = KeyConsts.Admin)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteStudentCommand { StudentId = id }, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
