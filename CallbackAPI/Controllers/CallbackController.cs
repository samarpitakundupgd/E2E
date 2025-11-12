using CallbackAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CallbackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallbackController : ControllerBase
    {
        private static CallbackRequestModel? _last;

        [HttpPost("case-request")]
        [HttpPost("caserequest")]   // optional alias
        [HttpPost("callrequest")]   // optional alias
        public IActionResult ReceiveCaseRequest([FromBody] CallbackRequestModel model)
        {
            _last = model;
            return Ok(new { message = "Callback received successfully." });
        }

        [HttpGet("last")]
        public IActionResult Last() => _last is null ? NotFound() : Ok(_last);
    }
}
