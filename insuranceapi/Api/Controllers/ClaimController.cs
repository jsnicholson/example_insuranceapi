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

        [HttpGet("{uniqueClaimReference}")]
        public async Task<ActionResult<GetClaimResponse>> Get([FromRoute] string uniqueClaimReference) {
            return Ok();
        }

        [HttpPatch("{uniqueClaimReference}")]
        public async Task<IActionResult> UpdateClaim([FromRoute] string uniqueClaimReference, [FromBody] ClaimController updatedClaim) {
            return Ok();
        }
    }
}