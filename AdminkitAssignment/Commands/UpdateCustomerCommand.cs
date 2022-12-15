using AdminkitAssignment.Database.Contexts;
using AdminkitAssignment.Database.Models;
using AdminkitAssignment.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace AdminkitAssignment.Commands
{
    public class UpdateCustomerCommand
    {
        private string? _connectionString;

        public UpdateCustomerCommand(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<CustomerInfo?> ExecuteAsync(int id, AddOrUpdateCustomerInput input)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using DbTransaction transaction = await connection.BeginTransactionAsync();

            ContactPhoneIdValue contactPhoneIdValue = await GetContactPhoneIdAsync(id, connection, transaction);
            if (contactPhoneIdValue == null)
            {
                return null;
            }

            await UpdateContactPhone(connection, transaction, input, contactPhoneIdValue.ContactPhoneId);
            await UpdateCustomer(connection, transaction, input, id);

            await transaction.CommitAsync();

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

        private async Task<ContactPhoneIdValue> GetContactPhoneIdAsync(
            int id,
            SqlConnection connection,
            DbTransaction transaction)
        {
            string contactPhoneIdQueryString = $"SELECT {nameof(Customer.ContactPhone)}Id " +
                $"FROM {nameof(CustomerContext.Customers)} " +
                $"WHERE {nameof(Customer.Id)} = {id}";

            return await connection.QuerySingleOrDefaultAsync<ContactPhoneIdValue>(contactPhoneIdQueryString, transaction: transaction);
        }

        private async Task UpdateContactPhone(
            SqlConnection connection,
            DbTransaction transaction,
            AddOrUpdateCustomerInput input,
            int contactPhoneId)
        {
            string commandText = $"Update {nameof(CustomerContext.CustomerContactPhones)} " +
                $"SET {nameof(CustomerContactPhone.Home)} = @home, " +
                $"{nameof(CustomerContactPhone.Work)} = @work, " +
                $"{nameof(CustomerContactPhone.Mobile)} = @mobile " +
                $"WHERE {nameof(CustomerContactPhone.Id)} = {contactPhoneId}";

            await connection.ExecuteAsync(commandText, new
            {
                home = input.HomePhone,
                work = input.WorkPhone,
                mobile = input.MobilePhone,
            }, transaction);
        }

        private async Task UpdateCustomer(
            SqlConnection connection,
            DbTransaction transaction,
            AddOrUpdateCustomerInput input,
            int id)
        {
            string commandText = $"Update {nameof(CustomerContext.Customers)} " +
                $"SET {nameof(Customer.Name)} = @name, " +
                $"{nameof(Customer.LastName)} = @lastName, " +
                $"{nameof(Customer.Email)} = @email, " +
                $"{nameof(Customer.Address)} = @address " +
                $"WHERE {nameof(Customer.Id)} = {id}";

            await connection.ExecuteAsync(commandText, new
            {
                name = input.Name,
                lastName = input.LastName,
                email = input.Email,
                address = input.Address
            }, transaction);
        }

        private class ContactPhoneIdValue
        {
            public int ContactPhoneId { get; set; }
        }
    }
}
