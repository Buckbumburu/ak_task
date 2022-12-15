using AdminkitAssignment.Database.Contexts;
using AdminkitAssignment.Database.Models;
using AdminkitAssignment.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace AdminkitAssignment.Commands
{
    public class GetCustomersCommand
    {
        private string? _connectionString;

        public GetCustomersCommand(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<List<CustomerInfo>> ExecuteAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            IEnumerable<Customer> customers = await connection
                .QueryAsync<Customer, CustomerContactPhone, Customer>(
                    $"SELECT " +
                    $"c.{nameof(Customer.Id)}, " +
                    $"c.{nameof(Customer.Name)}, " +
                    $"c.{nameof(Customer.LastName)}, " +
                    $"c.{nameof(Customer.Email)}, " +
                    $"c.{nameof(Customer.Address)}, " +
                    $"p.{nameof(CustomerContactPhone.Id)}, " +
                    $"p.{nameof(CustomerContactPhone.Home)}, " +
                    $"p.{nameof(CustomerContactPhone.Work)}, " +
                    $"p.{nameof(CustomerContactPhone.Mobile)} " +
                    $"FROM {nameof(CustomerContext.Customers)} c " +
                    $"INNER JOIN {nameof(CustomerContext.CustomerContactPhones)} p " +
                    $"ON c.{nameof(Customer.ContactPhone)}Id = p.{nameof(CustomerContactPhone.Id)}",
                    (customer, contactPhone) =>
                    {
                        customer.ContactPhone = contactPhone;
                        return customer;
                    });

            return customers
                .Select(x => new CustomerInfo
                {
                    Id = x.Id,
                    FullName = $"{x.Name} {x.LastName}",
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
