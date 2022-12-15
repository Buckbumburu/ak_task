using AdminkitAssignment.Validation;
using System.ComponentModel.DataAnnotations;

namespace AdminkitAssignment.Models
{
    public class AddOrUpdateCustomerInput
    {
        [Required]
        [RegularExpression("^([^0-9]*)$", ErrorMessage = "No numbers are allowed")]
        public required string Name { get; set; }
        
        [Required]
        [RegularExpression("^([^0-9]*)$", ErrorMessage = "No numbers are allowed")]
        public required string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        public required string Address { get; set; }

        [ContactPhoneRequired]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
        public string? HomePhone { get; set; }

        [ContactPhoneRequired]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
        public string? WorkPhone { get; set; }

        [ContactPhoneRequired]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
        public string? MobilePhone { get; set; }
    }
}
