using Api.Models;
using AutoMapper;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IClaimRepository _claimRepository;

        public ClaimController(ILogger<ClaimController> logger, IMapper mapper, IClaimRepository claimRepository) {
            _logger = logger;
            _mapper = mapper;
            _claimRepository = claimRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateClaim([FromBody] CreateClaimRequest requestClaim) {
            _logger.LogInformation($"{HttpContext.Request.Path.Value} received claim:{requestClaim}");
            if (requestClaim == null) return BadRequest("Must provide Claim");

            await _claimRepository.CreateClaimAsync(_mapper.Map<Claim>(requestClaim));

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Claim>>> Get() {
            _logger.LogInformation($"{HttpContext.Request.Path.Value}");
            var claims = await _claimRepository.GetAllClaimsAsync();

            if(claims == null || !claims.Any()) {
                _logger.LogInformation($"{HttpContext.Request.Path.Value} no claims were found");
                return NotFound("No claims found");
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
                return NotFound("No claims found");
            }

            var response = new GetClaimResponse(claim);
            _logger.LogInformation($"{HttpContext.Request.Path.Value} responding:{JsonSerializer.Serialize(response)}");
            return Ok(response);
        }

        [HttpPatch("{uniqueClaimReference}")]
        public async Task<ActionResult> UpdateClaim([FromRoute] string uniqueClaimReference, [FromBody] UpdateClaimRequest requestUpdateClaim) {
            _logger.LogInformation($"{HttpContext.Request.Path.Value} received requestUpdateClaim:{requestUpdateClaim}");
            var claim = await _claimRepository.GetClaimAsync(uniqueClaimReference);
            
            if (claim == null) return BadRequest("Claim does not exist");

            requestUpdateClaim.Update(ref claim);

            await _claimRepository.UpdateClaimAsync(claim);

            return Ok();
        }
    }
}