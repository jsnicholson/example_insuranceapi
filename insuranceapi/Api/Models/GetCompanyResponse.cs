using Data.Entities;

namespace Api.Models {
    public class GetCompanyResponse {
        public Company Company { get; set; }
        public bool HasActivePolicy { get; set; }

        public GetCompanyResponse(Company company) {
            this.Company = company;
            HasActivePolicy = company.Active && company.InsuranceEndDate < DateTime.Now;
        }
    }
}