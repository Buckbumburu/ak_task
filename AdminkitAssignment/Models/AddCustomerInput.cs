namespace AdminkitAssignment.Models
{
    public class AddCustomerInput
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public string? HomePhone { get; set; }
        public string? WorkPhone { get; set; }
        public string? MobilePhone { get; set; }
    }
}
