using System.Threading.Tasks;
using KooliProjekt.Application.Features.Soogikorrad;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class SoogikorradController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public SoogikorradController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListSoogikorradQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }
    }
}
