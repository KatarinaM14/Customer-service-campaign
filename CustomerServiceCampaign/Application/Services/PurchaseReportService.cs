using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.BaseModels;
using Infrastructure.Data.HelperClasses;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PurchaseReportService : IPurchaseReportService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CSVHelper _csvHelper = new CSVHelper();

        public PurchaseReportService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task ProcessCsvReportAsync(string csvFilePath)
        {
            try
            {
                
                var customers = _csvHelper.ReadCustomers(csvFilePath);

                foreach (var customer in customers)
                {
                    var existingCustomer = await _customerRepository.GetCustomerByExternalIdAsync(customer.ExternalId);

                    if (existingCustomer == null)
                    {
                        customer.AddedInMerge = true;
                        await _customerRepository.AddCustomerAsync(customer);
                    }

                    //if (existingCustomer != null)
                    //{
                    //    existingCustomer.IsRewarded = true; 
                    //    await _customerRepository.UpdateAsync(existingCustomer);
                    //}
                    //else
                    //{
                    //    await _customerRepository.AddAsync(record);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<byte[]> GenerateCsvAsync()
        {
            try
            {
                var customers = await _customerRepository.GetAllCustomersAsync();


                var fileBytes = await _csvHelper.GenerateCsvAsync((List<Customer>)customers);


                return fileBytes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
