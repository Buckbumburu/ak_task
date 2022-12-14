using AdminkitAssignment.Models;

namespace AdminkitAssignment.Commands
{
    public class AddCustomerCommand
    {
        public async Task<CustomerInfo> ExecuteAsync(AddCustomerInput input)
        {
            await Task.CompletedTask;
            return new CustomerInfo
            {
                Id = 1,
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
