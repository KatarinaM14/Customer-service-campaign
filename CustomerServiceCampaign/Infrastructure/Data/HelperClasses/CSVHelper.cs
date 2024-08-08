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

        public async Task<byte[]> GenerateCsvAsync(List<Customer> customers)
        {

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<GenerateCustomerCSVMap>();
                csv.WriteRecords(customers);
                writer.Flush();
                return memoryStream.ToArray();
            }
        }
    }
}
