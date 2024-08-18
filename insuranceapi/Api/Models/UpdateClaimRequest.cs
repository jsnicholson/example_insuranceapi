using Data.Entities;

namespace Api.Models {
    public class UpdateClaimRequest {
        public int? CompanyId { get; set; }
        public DateTime? ClaimDate { get; set; }
        public DateTime? LossDate { get; set; }
        public string? AssuredName { get; set; }
        public decimal? IncurredLoss { get; set; }
        public bool? Closed { get; set; }

        public void Update(ref Claim claim) {
            claim.CompanyId = CompanyId ?? claim.CompanyId;
            claim.ClaimDate = ClaimDate ?? claim.ClaimDate;
            claim.LossDate = LossDate ?? claim.LossDate;
            claim.AssuredName = AssuredName ?? claim.AssuredName;
            claim.IncurredLoss = IncurredLoss ?? claim.IncurredLoss;
            claim.Closed = Closed ?? claim.Closed;
        }
    }
}