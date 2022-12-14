using AdminkitAssignment.Models;

namespace AdminkitAssignment.Commands
{
    public class UpdateCustomerCommand
    {
        public async Task<CustomerInfo> ExecuteAsync(int id, AddOrUpdateCustomerInput input)
        {
            await Task.CompletedTask;
            return new CustomerInfo
            {
                Id = id,
                FullName = $"{input.Name} {input.LastName}",
                Address = input.Address,
                Email = input.Email,
                HomePhone = input.HomePhone,
                MobilePhone = input.MobilePhone,
                WorkPhone = input.WorkPhone
            };
        }
    }
}
