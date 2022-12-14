using AdminkitAssignment.Commands;
using AdminkitAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminkitAssignment.Controllers
{
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly AddCustomerCommand _addCustomer;
        private readonly GetCustomersCommand _getCustomers;
        private readonly GetCustomerByIdCommand _getCustomerById;
        private readonly UpdateCustomerCommand _updateCustomer;

        public CustomerController(
            AddCustomerCommand addCustomer,
            GetCustomersCommand getCustomers,
            GetCustomerByIdCommand getCustomerById,
            UpdateCustomerCommand updateCustomer)
        {
            _addCustomer = addCustomer;
            _getCustomers = getCustomers;
            _getCustomerById = getCustomerById;
            _updateCustomer = updateCustomer;
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

        [HttpPost]
        public async Task<CustomerInfo> AddCustomer([FromBody] AddOrUpdateCustomerInput input)
        {
            return await _addCustomer.ExecuteAsync(input);
        }

        [HttpPut("{id}")]
        public async Task<CustomerInfo> UpdateCustomer(int id, [FromBody] AddOrUpdateCustomerInput input)
        {
            return await _updateCustomer.ExecuteAsync(id, input);
        }
    }
}
