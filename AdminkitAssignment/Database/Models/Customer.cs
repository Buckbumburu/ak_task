using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminkitAssignment.Database.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public required int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(250)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(250)]
        public required string Address { get; set; }

        [Required]
        public required CustomerContactPhone ContactPhone { get; set; }
    }
}
