using Api.Models;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase {
        private readonly ILogger _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly IClaimRepository _claimRepository;

        public CompanyController(ILogger<CompanyController> logger, ICompanyRepository companyRepository, IClaimRepository claimRepository) {
            _logger = logger;
            _companyRepository = companyRepository;
            _claimRepository = claimRepository;
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<GetCompanyResponse>> Get(int companyId) {
            _logger.LogInformation($"{HttpContext.Request.Path.Value} received companyId:{companyId}");
            var company = await _companyRepository.GetCompanyAsync(companyId);

            if (company == null) {
                _logger.LogInformation($"{HttpContext.Request.Path.Value} companyId '{companyId}' was not found");
                return NotFound();
            }

            var response = new GetCompanyResponse(company);
            _logger.LogInformation($"{HttpContext.Request.Path.Value} responding:{JsonSerializer.Serialize(response)}");
            return Ok(response);
        }

        [HttpGet("{companyId}/claims")]
        public async Task<ActionResult<IEnumerable<Claim>>> GetClaims(int companyId) {
            _logger.LogInformation($"{HttpContext.Request.Path.Value} received companyId:{companyId}");
            var claims = await _claimRepository.GetClaimsForCompany(companyId);

            if(claims == null || !claims.Any()) {
                _logger.LogInformation($"{HttpContext.Request.Path.Value} companyId '{companyId}' found no claims");
                return NotFound();
            }

            _logger.LogInformation($"{HttpContext.Request.Path.Value} response:{JsonSerializer.Serialize(claims)1}");
            return Ok(claims);
        }
    }
}