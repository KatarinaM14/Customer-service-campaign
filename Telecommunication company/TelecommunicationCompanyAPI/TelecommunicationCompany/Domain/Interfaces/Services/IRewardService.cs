using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IRewardService
    {
        Task<bool> RewardCustomerAsync(int customerId, int userId, string description, decimal discountAmount);
        Task<byte[]> GenerateCsvAsync();
    }
}
