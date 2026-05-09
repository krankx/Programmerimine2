using System.Threading.Tasks;
using KooliProjekt.Application.Features.VeresuhkruMootmised;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class VeresuhkruMootmisedController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public VeresuhkruMootmisedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListVeresuhkruMootmisedQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetVeresuhkruMootmineQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaveVeresuhkruMootmineCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}
