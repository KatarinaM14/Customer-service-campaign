using CsvHelper.Configuration;
using Domain.Models.BaseModels;
using SharedProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mappings
{
    public class RewardDtoCSVMap : ClassMap<RewardDto>
    {
        public RewardDtoCSVMap() 
        {
            Map(m => m.Description).Name("Description");
            Map(m => m.DiscountAmount).Name("Discount amount");
            Map(m => m.CustomerId).Name("Customer id");
            Map(m => m.RewardingDate).Name("Rewarding date");
            Map(m => m.AgentFirstName).Name("Agent firstname");
            Map(m => m.AgentLastName).Name("Agent lastname");
        }
    }
}
