using AdminkitAssignment.Commands;
using AdminkitAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminkitAssignment.Controllers
{
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly AddCustomerCommand _addCustomer;
        private readonly DeleteCustomerCommand _deleteCustomer;
        private readonly GetCustomersCommand _getCustomers;
        private readonly GetCustomerByIdCommand _getCustomerById;
        private readonly UpdateCustomerCommand _updateCustomer;

        public CustomerController(
            AddCustomerCommand addCustomer,
            DeleteCustomerCommand deleteCustomer,
            GetCustomersCommand getCustomers,
            GetCustomerByIdCommand getCustomerById,
            UpdateCustomerCommand updateCustomer)
        {
            _addCustomer = addCustomer;
            _deleteCustomer = deleteCustomer;
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
        public async Task<IActionResult> GetCustomers(int id)
        {
            CustomerDetails? customer = await _getCustomerById.ExecuteAsync(id);

            if (customer!= null)
            {
                return Ok(customer);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] AddOrUpdateCustomerInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerInfo customer = await _addCustomer.ExecuteAsync(input);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] AddOrUpdateCustomerInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerInfo customer = await _updateCustomer.ExecuteAsync(id, input);
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (await _deleteCustomer.ExecuteAsync(id))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
