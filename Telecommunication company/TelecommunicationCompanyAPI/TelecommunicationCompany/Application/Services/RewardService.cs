using Domain.Interfaces;
using Domain.Interfaces.ExternalServices;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.BaseModels;
using Infrastructure.Data.HelperClasses;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RewardService : IRewardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerCampaignExternalService _customerCampaignExternalService;
        private readonly CSVHelper _csvHelper = new CSVHelper();

        public RewardService(IUnitOfWork unitOfWork, ICustomerCampaignExternalService customerCampaignExternalService)
        {
            _unitOfWork = unitOfWork;
            _customerCampaignExternalService = customerCampaignExternalService;
        }

        public async Task<bool> RewardCustomerAsync(int customerId, int userId, string description, decimal discountAmount)
        {
            try
            {
                var today = DateTime.Today;

                var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException("User not found.");
                }

                var rewardCount = await _unitOfWork.Rewards.GetRewardsByUserAsync(userId, today);

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

                await _unitOfWork.Rewards.AddRewardAsync(reward);
                await _unitOfWork.SaveChangesAsync();

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
                var rewards = await _unitOfWork.Rewards.GetAllRewardsAsync();

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
