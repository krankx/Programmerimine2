using System.Threading.Tasks;
using KooliProjekt.Application.Features.Kasutajad;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class KasutajadController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public KasutajadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListKasutajadQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetKasutajaQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaveKasutajaCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}
