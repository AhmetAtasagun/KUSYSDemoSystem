using KUSYS.Api.Controllers.Base;
using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.Business.Handlers.Students.Queries;
using KUSYS.Core.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Api.Controllers
{
    public class StudentController : BaseApiController
    {
        public StudentController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetList")]
        public async Task<List<StudentResponse>> GetList(CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetStudentsQuery(), cancellationToken);
        }

        [HttpGet("Get")]
        public async Task<StudentResponse> Get(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetStudentQuery { StudentId = id }, cancellationToken);
        }

        [HttpPost("Add")]
        public async Task<StudentResponse> Add(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpPut("Update")]
        public async Task<StudentResponse> Update(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("Delete")]
        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new DeleteStudentCommand { StudentId = id }, cancellationToken);
        }
    }
}
