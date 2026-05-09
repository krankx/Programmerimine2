using System.Threading.Tasks;
using KooliProjekt.Application.Features.KaaluMootmised;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class KaaluMootmisedController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public KaaluMootmisedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListKaaluMootmisedQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetKaaluMootmineQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaveKaaluMootmineCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}
