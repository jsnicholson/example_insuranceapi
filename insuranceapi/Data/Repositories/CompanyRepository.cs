using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories {
    public class CompanyRepository : ICompanyRepository {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ILogger<CompanyRepository> logger, ApplicationDbContext context) {
            _logger = logger;
            _context = context;
        }

        public async Task CreateCompanyAsync(Company company) {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCompaniesAsync(IEnumerable<Company> companies) {
            _context.Companies.AddRange(companies);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync() {
            return await _context.Companies.ToListAsync();
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(int companyId) {
            return await _context.Claims.Where(claim => claim.CompanyId == companyId).ToListAsync();
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(Company company) {
            return await _context.Claims.Where(claim => claim.CompanyId == company.Id).ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<int> companyIds) {
            return await _context.Companies.Where(c => companyIds.Contains(c.Id)).ToListAsync();
        }

        public async Task<Company?> GetCompanyAsync(int companyId) {
            return await _context.Companies.Where(c => c.Id == companyId).FirstOrDefaultAsync();
        }

        public async Task<Company?> GetCompanyAsync(string companyName) {
            companyName = companyName.ToLower();
            return await _context.Companies.Where(c => c.Name.ToLower().Contains(companyName)).FirstOrDefaultAsync();
        }
    }
}