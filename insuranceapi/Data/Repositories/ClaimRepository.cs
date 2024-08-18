using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories {
    public class ClaimRepository : IClaimRepository {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public ClaimRepository(ILogger<ClaimRepository> logger, ApplicationDbContext context) {
            _logger = logger;
            _context = context;
        }

        public async Task CreateClaimAsync(Claim claim) {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
        }

        public async Task CreateClaimAsync(IEnumerable<Claim> claims) {
            _context.Claims.AddRange(claims);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Claim>> GetAllClaimsAsync() {
            return await _context.Claims.ToListAsync();
        }

        public async Task<Claim?> GetClaimAsync(string claimId) {
            return await _context.Claims.Where(c => c.UniqueClaimReference.Equals(claimId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(IEnumerable<string> claimIds) {
            return await _context.Claims.Where(c => claimIds.Contains(c.UniqueClaimReference)).ToListAsync();
        }

        public async Task UpdateClaimAsync(Claim claim) {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Claim>> GetClaimsForCompanyAsync(int companyId) {
            return await _context.Claims.Where(c => c.CompanyId == companyId).ToListAsync();
        }
    }
}