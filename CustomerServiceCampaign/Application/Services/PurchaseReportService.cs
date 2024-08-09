using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.BaseModels;
using Infrastructure.Data.HelperClasses;
using SharedProject.DTOs;
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
                        customer.IsRewarded = true;
                        await _customerRepository.AddCustomerAsync(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<byte[]> MergeDataAsync(string csvFilePath)
        {
            try
            {
                var rewardedCustomers = new List<RewardedCustomerDto>();

                var rewards = _csvHelper.ReadRewardData(csvFilePath);

                foreach (var reward in rewards)
                {
                    var existingCustomer = await _customerRepository.GetCustomerByExternalIdAsync(reward.CustomerId);

                    if (existingCustomer != null)
                    {
                        var rewardedCustomer = new RewardedCustomerDto
                        {
                            Description = reward.Description,
                            DiscountAmount = reward.DiscountAmount,
                            CustomerId = reward.CustomerId,
                            RewardingDate = reward.RewardingDate,
                            AgentFirstName = reward.AgentFirstName,
                            AgentLastName = reward.AgentLastName,
                            Name = existingCustomer.Name,
                            SSN = existingCustomer.SSN,
                            DateOfBirth = existingCustomer.DateOfBirth,
                            Age = existingCustomer.Age,
                            IsRewarded = existingCustomer.IsRewarded,
                            Home = new AddressDTO { City = existingCustomer.Home.City, State = existingCustomer.Home.State, Street = existingCustomer.Home.Street, Zip = existingCustomer.Home.Zip },
                            Office = new AddressDTO { City = existingCustomer.Office.City, State = existingCustomer.Office.State, Street = existingCustomer.Office.Street, Zip = existingCustomer.Office.Zip },
                            FavoriteColors = existingCustomer.FavoriteColors.Select(c => new FavoriteColorDTO { Color = c.Color }).ToList()
                        };

                        rewardedCustomers.Add(rewardedCustomer);
                    }
                }

                var fileBytes = await _csvHelper.GenerateMergedDataCsvAsync(rewardedCustomers);

                return fileBytes;
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
