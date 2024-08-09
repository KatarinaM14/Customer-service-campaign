using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedProject.DTOs
{
    public class RewardDto
    {
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public int CustomerId { get; set; }
        public DateTime RewardingDate { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
    }
}
