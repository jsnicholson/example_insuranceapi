using Data.Entities;

namespace Api.Models {
    public class GetClaimResponse {
        public Claim claim { get; set; }
        public int DaysOld { get; set; }

        public GetClaimResponse(Claim claim) {
            this.claim = claim;
            DaysOld = (DateTime.Now - claim.ClaimDate).Days;
        }
    }
}