using Domain.Interfaces.ExternalServices;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.BaseModels;
using Infrastructure.Data.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RewardService : IRewardService
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerCampaignExternalService _customerCampaignExternalService;
        private readonly CSVHelper _csvHelper = new CSVHelper();

        public RewardService(IRewardRepository rewardRepository, IUserRepository userRepository, ICustomerCampaignExternalService customerCampaignExternalService) 
        {
            _rewardRepository = rewardRepository;
            _userRepository = userRepository;
            _customerCampaignExternalService = customerCampaignExternalService;
        }

        public async Task<bool> RewardCustomerAsync(int customerId, int userId, string description, decimal discountAmount)
        {
            try
            {

                var today = DateTime.Today;

                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException("User not found.");
                }

                var rewardCount = await _rewardRepository.GetRewardsByUserAsync(userId, today);

                if (rewardCount.Count() >= 5)
                {
                    return false;
                }

                var reward = new Reward
                {
                    CustomerId = customerId,
                    UserId = userId,
                    User = user,
                    Description = description,
                    DiscountAmount = discountAmount,
                    RewardingDate = DateTime.Now
                };

                await _rewardRepository.AddRewardAsync(reward);

                await _customerCampaignExternalService.NotifyCustomerCampaignExternalServiceAsync(customerId);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<byte[]> GenerateCsvAsync()
        {
            try
            {
                var rewards = await _rewardRepository.GetAllRewardsAsync();


                var fileBytes = await _csvHelper.GenerateCsvAsync((List<Reward>)rewards);


                return fileBytes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
