using CsvHelper;
using Domain.Models.BaseModels;
using Infrastructure.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.HelperClasses
{
    public class CSVHelper
    {
        public CSVHelper() { }

        public async Task<byte[]> GenerateCsvAsync(List<Reward> rewards)
        {

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<GenerateRewardCSVMap>();
                csv.WriteRecords(rewards);
                writer.Flush();
                return memoryStream.ToArray();
            }
        }
    }
}
