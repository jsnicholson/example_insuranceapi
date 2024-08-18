using Api.Models;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase {
        private readonly ILogger _logger;
        private readonly IClaimRepository _claimRepository;

        public ClaimController(ILogger<ClaimController> logger, IClaimRepository claimRepository) {
            _logger = logger;
            _claimRepository = claimRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Claim>>> Get() {
            _logger.LogInformation($"{HttpContext.Request.Path.Value}");
            var claims = await _claimRepository.GetAllClaimsAsync();

            if(claims == null || !claims.Any()) {
                _logger.LogInformation($"{HttpContext.Request.Path.Value} no claims were found");
                return NotFound();
            }

            _logger.LogInformation($"{HttpContext.Request.Path.Value} responding:{JsonSerializer.Serialize(claims)}");
            return Ok(claims);
        }

        [HttpGet("{uniqueClaimReference}")]
        public async Task<ActionResult<GetClaimResponse>> Get([FromRoute] string uniqueClaimReference) {
            _logger.LogInformation($"{HttpContext.Request.Path.Value} received uniqueClaimReference:{uniqueClaimReference}");
            var claim = await _claimRepository.GetClaimAsync(uniqueClaimReference);

            if (claim == null) {
                _logger.LogInformation($"{HttpContext.Request.Path.Value} uniqueClaimReference '{uniqueClaimReference}' found no claim");
                return NotFound();
            }

            var response = new GetClaimResponse(claim);
            _logger.LogInformation($"{HttpContext.Request.Path.Value} responding:{JsonSerializer.Serialize(response)}");
            return Ok(response);
        }

        [HttpPatch("{uniqueClaimReference}")]
        public async Task<IActionResult> UpdateClaim([FromRoute] string uniqueClaimReference, [FromBody] ClaimController updatedClaim) {
            return Ok();
        }
    }
}