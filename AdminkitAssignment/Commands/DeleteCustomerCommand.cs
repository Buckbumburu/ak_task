using AdminkitAssignment.Database.Contexts;
using AdminkitAssignment.Database.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace AdminkitAssignment.Commands
{
    public class DeleteCustomerCommand
    {
        private string? _connectionString;

        public DeleteCustomerCommand(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using DbTransaction transaction = await connection.BeginTransactionAsync();

            ContactPhoneIdValue contactPhoneIdValue = await GetContactPhoneIdAsync(id, connection, transaction);
            if (contactPhoneIdValue == null)
            {
                return false;
            }

            await DeleteContactPhone(contactPhoneIdValue.ContactPhoneId, connection, transaction);
            await DeleteCustomer(id, connection, transaction);

            await transaction.CommitAsync();
            return true;
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

        private async Task DeleteContactPhone(
            int contactPhoneId,
            SqlConnection connection,
            DbTransaction transaction)
        {
            string commandText = $"DELETE FROM " +
                $"{nameof(CustomerContext.CustomerContactPhones)} " +
                $"WHERE {nameof(CustomerContactPhone.Id)} = {contactPhoneId}";

            await connection.ExecuteAsync(commandText, transaction: transaction);
        }

        private async Task DeleteCustomer(
            int id,
            SqlConnection connection,
            DbTransaction transaction)
        {
            string commandText = $"DELETE FROM " +
                $"{nameof(CustomerContext.Customers)} " +
                $"WHERE {nameof(Customer.Id)} = {id}";

            await connection.ExecuteAsync(commandText, transaction: transaction);
        }

        private class ContactPhoneIdValue
        {
            public int ContactPhoneId { get; set; }
        }
    }
}
