using System.Threading.Tasks;
using KooliProjekt.Application.Features.VererohuMootmised;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class VererohuMootmisedController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public VererohuMootmisedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListVererohuMootmisedQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetVererohuMootmineQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaveVererohuMootmineCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}
