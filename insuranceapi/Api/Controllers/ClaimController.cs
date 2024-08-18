using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase {
        private readonly ILogger _logger;

        public ClaimController(ILogger<ClaimController> logger) {
            _logger = logger;
        }

        [HttpGet("{ucr}")]
        public async Task<ActionResult<GetClaimResponse>> Get(string ucr) {
            return Ok();
        }

        [HttpPatch("{ucr}")]
        public async Task<IActionResult> UpdateClaim(string ucr, [FromBody] ClaimController updatedClaim) {
            return Ok();
        }
    }
}