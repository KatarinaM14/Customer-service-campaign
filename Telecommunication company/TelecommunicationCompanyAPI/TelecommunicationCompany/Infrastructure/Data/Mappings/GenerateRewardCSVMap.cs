using CsvHelper.Configuration;
using Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mappings
{
    public class GenerateRewardCSVMap : ClassMap<Reward>
    {
        public GenerateRewardCSVMap() 
        {
            Map(m => m.Description).Name("Description");
            Map(m => m.DiscountAmount).Name("Discount amount");
            Map(m => m.CustomerId).Name("Customer id");
            Map(m => m.RewardingDate).Name("Rewarding date");

            Map(m => m.User.FirstName).Name("Agent firstname");
            Map(m => m.User.LastName).Name("Agent lastname");
        }
    }
}
