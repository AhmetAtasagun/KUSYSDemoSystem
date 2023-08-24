using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.Business.Handlers.Students.Queries;
using KUSYS.Core.Models.Response;
using KUSYS.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Web.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await Mediator.Send(new GetStudentsQuery(), cancellationToken));
        }

        [HttpGet]
        public async Task<StudentResponse> Get(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetStudentQuery { StudentId = id }, cancellationToken);
        }

        [HttpPost]
        public async Task<StudentResponse> Add(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public async Task<StudentResponse> Update(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new DeleteStudentCommand { StudentId = id }, cancellationToken);
        }
    }
}
