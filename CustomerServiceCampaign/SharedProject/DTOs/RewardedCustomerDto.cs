using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedProject.DTOs
{
    public class RewardedCustomerDto
    {
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public int CustomerId { get; set; }
        public DateTime RewardingDate { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressDTO Home { get; set; }
        public AddressDTO Office { get; set; }
        public List<FavoriteColorDTO> FavoriteColors { get; set; } = new List<FavoriteColorDTO>();
        public int Age { get; set; }
        public bool IsRewarded { get; set; }
    }
}
