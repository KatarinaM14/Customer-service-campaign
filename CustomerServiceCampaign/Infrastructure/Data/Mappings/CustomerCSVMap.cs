using CsvHelper.Configuration;
using Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mappings
{
    public class CustomerCSVMap : ClassMap<Customer>
    {
        public CustomerCSVMap() 
        {
            Map(m => m.ExternalId).Name("Id");
            Map(m => m.Name).Name("Name");
            Map(m => m.SSN).Name("SSN");
            Map(m => m.DateOfBirth).Name("DateOfBirth");
            Map(m => m.Age).Name("Age");
            Map(m => m.IsRewarded).Name("IsRewarded");

            Map(m => m.Home.Street).Name("HomeStreet");
            Map(m => m.Home.City).Name("HomeCity");
            Map(m => m.Home.State).Name("HomeState");
            Map(m => m.Home.Zip).Name("HomeZip");

            Map(m => m.Office.Street).Name("OfficeStreet");
            Map(m => m.Office.City).Name("OfficeCity");
            Map(m => m.Office.State).Name("OfficeState");
            Map(m => m.Office.Zip).Name("OfficeZip");

            Map(m => m.FavoriteColors).TypeConverter<FavoriteColorsConverter>().Name("Color");
        }
    }
}
