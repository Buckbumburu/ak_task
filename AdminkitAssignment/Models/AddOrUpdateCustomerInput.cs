using AdminkitAssignment.Validation;
using System.ComponentModel.DataAnnotations;

namespace AdminkitAssignment.Models
{
    public class AddOrUpdateCustomerInput
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression("^([^0-9]*)$", ErrorMessage = "No numbers are allowed")]
        public required string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        [RegularExpression("^([^0-9]*)$", ErrorMessage = "No numbers are allowed")]
        public required string LastName { get; set; }
        
        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        [MaxLength(250)]
        public required string Address { get; set; }

        [ContactPhoneRequired]
        [MaxLength(50)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
        public string? HomePhone { get; set; }

        [ContactPhoneRequired]
        [MaxLength(50)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
        public string? WorkPhone { get; set; }

        [ContactPhoneRequired]
        [MaxLength(50)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
        public string? MobilePhone { get; set; }
    }
}
