using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BaseModels
{
    public class Reward
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime RewardingDate { get; set; }
    }
}
