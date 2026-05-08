using System.Threading.Tasks;
using KooliProjekt.Application.Features.SoogikorraRead;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class SoogikorraReadController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public SoogikorraReadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListSoogikorraReadQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }
    }
}
