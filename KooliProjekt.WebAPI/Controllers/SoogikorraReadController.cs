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
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListSoogikorraReadQuery query)
        {
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetSoogikorraRidaQuery { Id = id };
            var response = await _mediator.Send(query);
            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaveSoogikorraRidaCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteSoogikorraRidaCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}
