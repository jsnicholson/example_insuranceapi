using Data.Entities;

namespace Api.Models {
    public class GetClaimResponse {
        public Claim Claim { get; set; }
        public int DaysOld { get; set; }

        public GetClaimResponse(Claim claim) {
            this.Claim = claim;
            DaysOld = (DateTime.Now - claim.ClaimDate).Days;
        }
    }
}