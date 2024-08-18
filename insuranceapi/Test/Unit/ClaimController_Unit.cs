using Api.Controllers;
using Api.Models;
using AutoMapper;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Unit {
    public class ClaimController_Unit {
        [Fact]
        public async Task Get_WhereClaimExists_ReturnsClaimDetails() {
            var ucr = "abcd";
            var logger = new Mock<ILogger<ClaimController>>();
            var expectedClaim = new Claim() {
                CompanyId = 1,
                UniqueClaimReference = ucr,
                ClaimDate = DateTime.Now.AddDays(-1)
            };
            var expectedClaimDetails = new GetClaimResponse(expectedClaim);

            var claimRepository = new Mock<IClaimRepository>();
            claimRepository.Setup(repo => repo.GetClaimAsync(ucr)).ReturnsAsync(expectedClaim);
            var mapper = new Mock<IMapper>();
            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext() { HttpContext = httpContext };

            var controller = new ClaimController(logger.Object, mapper.Object, claimRepository.Object) {
                ControllerContext = controllerContext
            };

            var result = await controller.Get(ucr);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var claimDetails = Assert.IsAssignableFrom<GetClaimResponse>(okResult.Value);
            Assert.Equal(expectedClaim.CompanyId, claimDetails.Claim.CompanyId);
            Assert.Equal(1, claimDetails.DaysOld);
        }

        [Fact]
        public async Task UpdateClaim_WhereClaimExists_Success() {
            var logger = new Mock<ILogger<ClaimController>>();
            var claim = new Claim() {
                UniqueClaimReference = "abcd",
                IncurredLoss = 200
            };
            var updateClaim = new UpdateClaimRequest() {
                IncurredLoss = 300
            };

            var claimRepository = new Mock<IClaimRepository>();
            claimRepository.Setup(repo => repo.GetClaimAsync(claim.UniqueClaimReference)).ReturnsAsync(claim);
            var mapper = new Mock<IMapper>();
            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext() { HttpContext = httpContext };

            var controller = new ClaimController(logger.Object, mapper.Object, claimRepository.Object) {
                ControllerContext = controllerContext
            };

            var result = await controller.UpdateClaim(claim.UniqueClaimReference, updateClaim);
            claim.IncurredLoss = (decimal)updateClaim.IncurredLoss;

            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateClaim_WhereClaimDoesNotExist_Fail() {
            var logger = new Mock<ILogger<ClaimController>>();
            var claim = new Claim() {
                UniqueClaimReference = "abcd",
                IncurredLoss = 200
            };
            var updateClaim = new UpdateClaimRequest() {
                IncurredLoss = 300
            };

            var claimRepository = new Mock<IClaimRepository>();
            claimRepository.Setup(repo => repo.GetClaimAsync(claim.UniqueClaimReference)).ReturnsAsync((Claim?)null);
            var mapper = new Mock<IMapper>();
            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext() { HttpContext = httpContext };

            var controller = new ClaimController(logger.Object, mapper.Object, claimRepository.Object) {
                ControllerContext = controllerContext
            };

            var result = await controller.UpdateClaim(claim.UniqueClaimReference, updateClaim);
            claim.IncurredLoss = (decimal)updateClaim.IncurredLoss;

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
