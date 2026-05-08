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
        public async Task<IActionResult> List([FromQuery] ListKasutajadQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }
    }
}
