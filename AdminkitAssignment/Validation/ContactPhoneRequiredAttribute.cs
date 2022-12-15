using AdminkitAssignment.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AdminkitAssignment.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ContactPhoneRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectType != typeof(AddOrUpdateCustomerInput))
            {
                return new ValidationResult($"Validation attribute can only be used on properties of {nameof(AddOrUpdateCustomerInput)}");
            }

            PropertyInfo homePhoneProperty = validationContext
                .ObjectType
                .GetProperty(nameof(AddOrUpdateCustomerInput.HomePhone))!;

            PropertyInfo workPhoneProperty = validationContext
                .ObjectType
                .GetProperty(nameof(AddOrUpdateCustomerInput.WorkPhone))!;

            PropertyInfo mobilePhoneProperty = validationContext
              .ObjectType
              .GetProperty(nameof(AddOrUpdateCustomerInput.MobilePhone))!;

            string? homePhone = homePhoneProperty.GetValue(validationContext.ObjectInstance) as string;
            string? workPhone = workPhoneProperty.GetValue(validationContext.ObjectInstance) as string;
            string? mobilePhone= mobilePhoneProperty.GetValue(validationContext.ObjectInstance) as string;

            if (!string.IsNullOrEmpty(homePhone) || !string.IsNullOrEmpty(workPhone) || !string.IsNullOrEmpty(mobilePhone))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("At least one contact phone is required");
        }
    }
}
