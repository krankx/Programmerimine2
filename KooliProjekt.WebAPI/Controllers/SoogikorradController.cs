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
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListSoogikorradQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetSoogikordQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaveSoogikordCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteSoogikordCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}
