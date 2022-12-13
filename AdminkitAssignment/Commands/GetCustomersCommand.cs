using AdminkitAssignment.Models;

namespace AdminkitAssignment.Commands
{
    public class GetCustomersCommand
    {
        public async Task<List<CustomerInfo>> ExecuteAsync()
        {
            await Task.CompletedTask;

            var customers = new List<Customer>
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
                },
                new Customer
                {
                    Name = "Test2",
                    LastName = "Test2",
                    Address = "Test Road 124",
                    Email = "test2@test.test",
                    ContactPhone = new CustomerContactPhone
                    {
                        Home = "12345679",
                        Work = "665544"
                    }
                }
            };

            return customers
                .Select(x => new CustomerInfo
                {
                    FullName = x.Name,
                    Address = x.Address,
                    Email = x.Email,
                    HomePhone = x.ContactPhone.Home,
                    WorkPhone = x.ContactPhone.Work,
                    MobilePhone = x.ContactPhone.Mobile,
                })
                .ToList();
        }
    }
}
