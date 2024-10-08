﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities {
    [Table("Claims")]
    public class Claim {
        [Key]
        [MaxLength(20)]
        [Column("UCR")]
        public string UniqueClaimReference { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        [Column("Assured Name")]
        [MaxLength(100)]
        public string AssuredName { get; set; }
        [Column("Incurred Loss")]
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
    }
}
