using Data.Entities;

namespace Data.Repositories.Interfaces {
    public interface IClaimRepository {
        Task<Claim?> GetClaimAsync(string claimId);
        Task<IEnumerable<Claim>> GetClaimsAsync(IEnumerable<string> claimIds);
        Task<IEnumerable<Claim>> GetAllClaimsAsync();
        Task UpdateClaimAsync(Claim claim);
    }
}