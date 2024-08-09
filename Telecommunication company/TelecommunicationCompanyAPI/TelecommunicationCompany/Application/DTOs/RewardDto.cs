using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RewardDto
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
    }
}
