using Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRewardRepository
    {
        Task<Reward> GetRewardByIdAsync(int id);
        Task<IEnumerable<Reward>> GetRewardsByUserAsync(int userId, DateTime dateOfRewarding);
        Task AddRewardAsync(Reward reward);
    }
}
