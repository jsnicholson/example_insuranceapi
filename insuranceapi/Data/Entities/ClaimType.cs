using System.ComponentModel.DataAnnotations;

namespace Data.Entities {
    public class ClaimType {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
    }
}