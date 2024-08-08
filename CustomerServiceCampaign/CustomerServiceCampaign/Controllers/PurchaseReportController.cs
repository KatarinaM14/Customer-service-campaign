using Domain.Interfaces.ExternalServices;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceCampaign.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseReportController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IPurchaseReportService _purchaseReportService;

        public PurchaseReportController(ICustomerService customerService, IPurchaseReportService purchaseReportService)
        {
            _customerService = customerService;
            _purchaseReportService = purchaseReportService;
        }

        [HttpPost("UploadReport")]
        public async Task<IActionResult> UploadCsvReport(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

                await _purchaseReportService.ProcessCsvReportAsync(filePath);

                return Ok("Report processed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GenerateCSVFile")]
        public async Task<IActionResult> ExportCustomersToCsv()
        {
            try
            {
                var csvData = await _purchaseReportService.GenerateCsvAsync();

                return File(csvData, "text/csv", "customers.csv");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }
    }
}
