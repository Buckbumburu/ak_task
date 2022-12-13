using AdminkitAssignment.Commands;
using AdminkitAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminkitAssignment.Controllers
{
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly GetCustomersCommand _getCustomers;

        public CustomerController(GetCustomersCommand getCustomers)
        {
            _getCustomers = getCustomers;
        }

        [HttpGet]
        public async Task<List<Customer>> GetCustomers()
        {
            return await _getCustomers.ExecuteAsync();
        }
    }
}
