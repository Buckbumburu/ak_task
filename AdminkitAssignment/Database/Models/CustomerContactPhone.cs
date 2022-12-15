using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminkitAssignment.Database.Models
{
    public class CustomerContactPhone
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Home { get; set; }

        [MaxLength(50)]
        public string? Work { get; set; }

        [MaxLength(50)]
        public string? Mobile { get; set; }
    }
}
