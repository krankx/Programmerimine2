using System.Threading.Tasks;
using KooliProjekt.Application.Features.Toiduained;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class ToiduainedController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public ToiduainedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListToiduainedQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetToiduaineQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaveToiduaineCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}
