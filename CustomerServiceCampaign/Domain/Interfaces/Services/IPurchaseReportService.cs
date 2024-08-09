using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IPurchaseReportService
    {
        Task ProcessCsvReportAsync(string csvFilePath);

        Task<byte[]> MergeDataAsync(string csvFilePath);

        Task<byte[]> GenerateCsvAsync();
    }
}
