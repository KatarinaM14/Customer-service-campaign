using CsvHelper.Configuration;
using SharedProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mappings
{
    public class GenerateRewardedCustomerCSVMap : ClassMap<RewardedCustomerDto>
    {
        public GenerateRewardedCustomerCSVMap()
        {
            Map(m => m.Description).Name("Description");
            Map(m => m.DiscountAmount).Name("Discount amount");
            Map(m => m.CustomerId).Name("Customer id");
            Map(m => m.RewardingDate).Name("Rewarding date");

            Map(m => m.AgentLastName).Name("Agent firstname");
            Map(m => m.AgentLastName).Name("Agent lastname");

            Map(m => m.Name).Name("Name");
            Map(m => m.SSN).Name("SSN");
            Map(m => m.DateOfBirth).Name("Birth date");
            Map(m => m.Age).Name("Age");
            Map(m => m.IsRewarded).Name("Is rewarded");

            Map(m => m.Home.Street).Name("Home street");
            Map(m => m.Home.City).Name("Home city");
            Map(m => m.Home.State).Name("Home state");
            Map(m => m.Home.Zip).Name("Home zip");

            Map(m => m.Office.Street).Name("Office street");
            Map(m => m.Office.City).Name("Office city");
            Map(m => m.Office.State).Name("Office state");
            Map(m => m.Office.Zip).Name("Office zip");

            Map(m => m.FavoriteColors).TypeConverter<GenerateFavoriteColorsDtoConverter>().Name("Favorite colors");
        }
    }
}
