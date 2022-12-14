namespace AdminkitAssignment.Models
{
    public class Customer
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required CustomerContactPhone ContactPhone { get; set; }
    }
}
