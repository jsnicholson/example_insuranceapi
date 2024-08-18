using Data.Entities;

namespace Data.Repositories.Interfaces {
    public interface ICompanyRepository {
        Task CreateCompanyAsync(Company company);
        Task CreateCompaniesAsync(IEnumerable<Company> companies);
        Task<Company?> GetCompanyAsync(int companyId);
        Task<Company?> GetCompanyAsync(string companyName);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<int> companyIds);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<IEnumerable<Claim>> GetClaimsAsync(int companyId);
        Task<IEnumerable<Claim>> GetClaimsAsync(Company company);
    }
}