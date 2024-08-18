using Data.Entities;

namespace Api.Models {
    public class GetClaimResponse : Claim {
        public int DaysOld { get; set; }
    }
}