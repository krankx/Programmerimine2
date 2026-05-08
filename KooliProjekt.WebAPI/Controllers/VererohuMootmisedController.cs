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
        public async Task<IActionResult> List([FromQuery] ListVererohuMootmisedQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }
    }
}
