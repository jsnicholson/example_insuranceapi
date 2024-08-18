using Api.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase {
        private readonly ILogger _logger;

        public CompanyController(ILogger<CompanyController> logger) {
            _logger = logger;
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<GetCompanyResponse>> Get(int companyId) {
            return Ok();
        }

        [HttpGet("{companyId}/claims")]
        public async Task<ActionResult<IEnumerable<Claim>>> GetClaims(int companyId) {
            return Ok();
        }
    }
}