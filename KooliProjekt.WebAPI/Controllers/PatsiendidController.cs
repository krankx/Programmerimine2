using System.Threading.Tasks;
using KooliProjekt.Application.Features.Patsiendid;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class PatsiendidController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public PatsiendidController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListPatsiendidQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetPatsientQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SavePatsientCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}
