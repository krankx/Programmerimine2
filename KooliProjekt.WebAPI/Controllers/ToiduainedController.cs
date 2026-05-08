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
        public async Task<IActionResult> List()
        {
            var query = new ListToiduainedQuery();
            var result = await _mediator.Send(query);
            return Result(result);
        }
    }
}
