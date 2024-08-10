using Domain.Interfaces.Repositories;
using Domain.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RewardRepository : IRewardRepository
    {
        private readonly ApplicationDbContext _context;

        public RewardRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Reward>> GetAllRewardsAsync()
        {
            return await _context.Rewards
                    .Include(r => r.User)
                    .ToListAsync(); 
        }

        public async Task<Reward> GetRewardByIdAsync(int id)
        {
            return await _context.Rewards.FindAsync(id);
        }

        public async Task<IEnumerable<Reward>> GetRewardsByUserAsync(int userId, DateTime dateOfRewarding)
        {
            return await _context.Rewards
                .Where(r => r.UserId == userId && r.RewardingDate.Date == dateOfRewarding.Date)
                .ToListAsync();
        }

        public async Task AddRewardAsync(Reward reward)
        {
            _context.Rewards.Add(reward);
        }
    }
}
