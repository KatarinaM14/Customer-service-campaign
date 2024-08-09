using Domain.Interfaces.ExternalServices;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceCampaign.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IUserExternalService _userExternalService;

        public CustomerController(ICustomerService customerService, IUserExternalService userExternalService)
        {
            _customerService = customerService;
            _userExternalService = userExternalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("RewardCustomer")]
        public async Task<IActionResult> RewardCustomer([FromBody] int id)
        {
            try
            {
                var customer = await _userExternalService.GetUserByIdAsync(id);

                if (customer == null)
                {
                    return NotFound("User not found");
                }
            
                await _customerService.RewardCustomerAsync(customer);
       
                return Ok("User rewarded");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
