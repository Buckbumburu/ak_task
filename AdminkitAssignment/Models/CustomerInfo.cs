namespace AdminkitAssignment.Models
{
    public class CustomerInfo
    {
        public required int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public string? HomePhone { get; set; }
        public string? WorkPhone { get; set; }
        public string? MobilePhone { get; set; }
    }
}
