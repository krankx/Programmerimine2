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
        public async Task<IActionResult> List([FromQuery] ListKaaluMootmisedQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }
    }
}
