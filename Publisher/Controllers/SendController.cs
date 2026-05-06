using Event.Bus;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public SendController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("/send")]
        public async Task<IActionResult> Send([FromBody] RentMovieEvent msg)
        {
            await _publishEndpoint.Publish<RentMovieEvent>(msg);
            return Ok();
        }
    }
}
