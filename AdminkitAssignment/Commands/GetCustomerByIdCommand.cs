using AdminkitAssignment.Models;

namespace AdminkitAssignment.Commands
{
    public class GetCustomerByIdCommand
    {
        public async Task<CustomerDetails> ExecuteAsync(int id)
        {
            await Task.CompletedTask;
            return new CustomerDetails
            {
                Id = id,
                Name = "Test",
                LastName = "Test",
                Address = "Test Road 123",
                Email = "test@test.test",
                WorkPhone = "12345678"
            };
        }
    }
}
