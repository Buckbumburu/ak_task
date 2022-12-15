using AdminkitAssignment.Database.Contexts;
using AdminkitAssignment.Database.Models;
using AdminkitAssignment.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace AdminkitAssignment.Commands
{
    public class GetCustomerByIdCommand
    {
        private string? _connectionString;

        public GetCustomerByIdCommand(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<CustomerDetails?> ExecuteAsync(int id)
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
                   $"ON c.{nameof(Customer.ContactPhone)}Id = p.{nameof(CustomerContactPhone.Id)} " +
                   $"WHERE c.{nameof(Customer.Id)} = {id}",
                   (customer, contactPhone) =>
                   {
                       customer.ContactPhone = contactPhone;
                       return customer;
                   });

            Customer? customer = customers.FirstOrDefault();

            return customer != null
                ? new CustomerDetails
                {
                    Id = id,
                    Name = customer.Name,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Email = customer.Email,
                    HomePhone = customer.ContactPhone.Home,
                    WorkPhone = customer.ContactPhone.Work,
                    MobilePhone = customer.ContactPhone.Mobile,
                }
                : null;
        }
    }
}
