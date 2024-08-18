using Data.Entities;

namespace Data.Repositories.Interfaces {
    public interface ICompanyRepository {
        Task<Company?> GetCompanyAsync(int companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<int> companyIds);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<IEnumerable<Claim>> GetClaimsAsync(int companyId);
        Task<IEnumerable<Claim>> GetClaimsAsync(Company company);
    }
}