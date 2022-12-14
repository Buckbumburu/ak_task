using AdminkitAssignment.Commands;
using AdminkitAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminkitAssignment.Controllers
{
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly GetCustomersCommand _getCustomers;
        private readonly GetCustomerByIdCommand _getCustomerById;

        public CustomerController(
            GetCustomersCommand getCustomers,
            GetCustomerByIdCommand getCustomerById)
        {
            _getCustomers = getCustomers;
            _getCustomerById = getCustomerById;
        }

        [HttpGet]
        public async Task<List<CustomerInfo>> GetCustomers()
        {
            return await _getCustomers.ExecuteAsync();
        }

        [HttpGet("{id}")]
        public async Task<CustomerDetails> GetCustomers(int id)
        {
            return await _getCustomerById.ExecuteAsync(id);
        }
    }
}
