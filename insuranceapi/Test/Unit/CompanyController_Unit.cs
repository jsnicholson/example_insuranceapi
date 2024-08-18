using Api.Controllers;
using Api.Models;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Unit {
    public class CompanyController_Unit {
        [Fact]
        public async Task GetCompanyById_ReturnsCompany_WhenCompanyExists() {
            var companyId = 1;
            var logger = new Mock<ILogger<CompanyController>>();
            var expectedCompany = new Company { Id = companyId, Name = "Test Company", Active = true, InsuranceEndDate = DateTime.Now.AddDays(1) };
            var companyRepository = new Mock<ICompanyRepository>();
            companyRepository.Setup(repo => repo.GetCompanyAsync(companyId)).ReturnsAsync(expectedCompany);
            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext() { HttpContext = httpContext };
            var claimRepository = new Mock<IClaimRepository>();

            var controller = new CompanyController(logger.Object, companyRepository.Object, claimRepository.Object) {
                ControllerContext = controllerContext
            };

            var result = await controller.Get(companyId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var company = Assert.IsType<GetCompanyResponse>(okResult.Value);
            Assert.Equal(expectedCompany.Id, company.Company.Id);
            Assert.Equal(expectedCompany.Name, company.Company.Name);
            Assert.Equal(true, company.HasActivePolicy);
        }

        [Fact]
        public async Task GetClaims_ReturnsClaims_WhereClaimsExist() {
            var companyId = 1;
            var logger = new Mock<ILogger<CompanyController>>();
            var expectedClaims = new List<Claim>() {
                new() {
                    CompanyId = 1,
                    UniqueClaimReference = "abcd"
                }
            };
            var companyRepository = new Mock<ICompanyRepository>();
            var claimRepository = new Mock<IClaimRepository>();
            claimRepository.Setup(repo => repo.GetClaimsForCompanyAsync(companyId)).ReturnsAsync(expectedClaims);
            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext() { HttpContext = httpContext };

            var controller = new CompanyController(logger.Object, companyRepository.Object, claimRepository.Object) {
                ControllerContext = controllerContext
            };

            var result = await controller.GetClaims(companyId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var claims = Assert.IsAssignableFrom<IEnumerable<Claim>>(okResult.Value);
            Assert.Equal(expectedClaims.First().CompanyId, claims.First().CompanyId);
            Assert.Equal(expectedClaims.First().UniqueClaimReference, claims.First().UniqueClaimReference);
        }
    }
}