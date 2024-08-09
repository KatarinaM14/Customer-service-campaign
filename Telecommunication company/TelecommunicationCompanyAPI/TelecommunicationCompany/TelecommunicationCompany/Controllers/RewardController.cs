using Application.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TelecommunicationCompany.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RewardController : ControllerBase
    {
        private readonly IRewardService _rewardService;

        public RewardController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpPost]
        [Route("RewardCustomer")]
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> RewardCustomer([FromBody] RewardDto rewardDto)
        {
            try
            {
                if (rewardDto.CustomerId == 0 || rewardDto.UserId == 0 || rewardDto.DiscountAmount == 0 || string.IsNullOrEmpty(rewardDto.Description))
                {
                    return BadRequest("Invalid data!");
                }
                var success = await _rewardService.RewardCustomerAsync(rewardDto.CustomerId, rewardDto.UserId, rewardDto.Description, rewardDto.DiscountAmount);

                if (success)
                {
                    return Ok("Reward issued successfully.");
                }
                else
                {
                    return BadRequest("Daily reward limit exceeded for the agent.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
