using AdminkitAssignment.Models;

namespace AdminkitAssignment.Commands
{
    public class GetCustomersCommand
    {
        public async Task<List<Customer>> ExecuteAsync()
        {
            await Task.CompletedTask;

            return new List<Customer>
            {
                new Customer
                {
                    Name = "Test",
                    LastName = "Test",
                    Address = "Test Road 123",
                    Email = "test@test.test",
                    ContactPhone = new CustomerContactPhone
                    {
                        Home = "12345678"
                    }
                }
            };
        }
    }
}
