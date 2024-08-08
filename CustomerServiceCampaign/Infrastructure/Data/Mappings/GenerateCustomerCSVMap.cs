using CsvHelper.Configuration;
using Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mappings
{
    public class GenerateCustomerCSVMap : ClassMap<Customer>
    {
        public GenerateCustomerCSVMap() 
        {
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

            Map(m => m.FavoriteColors).TypeConverter<GenerateFavoriteColorsConverter>().Name("Favorite colors");
        }
    }
}
