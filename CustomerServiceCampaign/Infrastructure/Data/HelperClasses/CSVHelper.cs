using CsvHelper;
using Domain.Models.BaseModels;
using Infrastructure.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.HelperClasses
{
    public class CSVHelper
    {
        public CSVHelper() { }

        public List<Customer> ReadCustomers(string csvFilePath)
        {
            var customers = new List<Customer>();

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<CustomerCSVMap>();
                customers = csv.GetRecords<Customer>().ToList();
            }

            return customers;
        }
    }
}
