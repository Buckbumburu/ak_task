using AdminkitAssignment.Database.Contexts;
using AdminkitAssignment.Database.Models;
using AdminkitAssignment.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace AdminkitAssignment.Commands
{
    public class AddCustomerCommand
    {
        private string? _connectionString;

        public AddCustomerCommand(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<CustomerInfo> ExecuteAsync(AddOrUpdateCustomerInput input)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using DbTransaction transaction = await connection.BeginTransactionAsync();

            int customeContactPhoneId = await AddCustomerContactPhoneAsync(connection, transaction, input);
            int customerId = await AddCustomerAsync(connection, transaction, input, customeContactPhoneId);
           
            await transaction.CommitAsync();

            return new CustomerInfo
            {
                Id = customerId,
                FullName = $"{input.Name} {input.LastName}",
                Address = input.Address,
                Email = input.Email,
                HomePhone = input.HomePhone,
                MobilePhone = input.MobilePhone,
                WorkPhone = input.WorkPhone
            };
        }

        private async Task<int> AddCustomerContactPhoneAsync(
            SqlConnection connection,
            DbTransaction transaction,
            AddOrUpdateCustomerInput input)
        {
            string commandText = $"INSERT INTO {nameof(CustomerContext.CustomerContactPhones)} (" +
                $"{nameof(CustomerContactPhone.Home)}, " +
                $"{nameof(CustomerContactPhone.Work)}, " +
                $"{nameof(CustomerContactPhone.Mobile)}) " +
                $"OUTPUT INSERTED.{nameof(CustomerContactPhone.Id)} " +
                "VALUES (@home, @work, @mobile)";

            return await connection.ExecuteScalarAsync<int>(commandText, new
            {
                home = input.HomePhone,
                work = input.WorkPhone,
                mobile = input.MobilePhone,
            }, transaction);
        }

        private async Task<int> AddCustomerAsync(
            SqlConnection connection,
            DbTransaction transaction,
            AddOrUpdateCustomerInput input,
            int customerContactPhoneId)
        {
            string commandText = $"INSERT INTO {nameof(CustomerContext.Customers)} (" +
                $"{nameof(Customer.Name)}, " +
                $"{nameof(Customer.LastName)}, " +
                $"{nameof(Customer.Email)}, " +
                $"{nameof(Customer.Address)}, " +
                $"{nameof(Customer.ContactPhone)}Id) " +
                $"OUTPUT INSERTED.{nameof(CustomerContactPhone.Id)} " +
                "VALUES (@name, @lastName, @email, @address, @contactPhoneId)";

            return await connection.ExecuteScalarAsync<int>(commandText, new
            {
                name = input.Name,
                lastName = input.LastName,
                email = input.Email,
                address = input.Address,
                contactPhoneId = customerContactPhoneId
            }, transaction);
        }
    }
}
