using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.BaseModels;
using SharedProject.DTOs;
using System.Data;

namespace CustomerServiceCampaign.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) 
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task RewardCustomerAsync(CustomerDTO customerDTO)
        {
            try
            {
                var customer = new Customer
                {
                    Name = customerDTO.Name,
                    SSN = customerDTO.SSN,
                    DateOfBirth = customerDTO.DateOfBirth,
                    Home = new Address
                    {
                        Street = customerDTO.Home.Street,
                        City = customerDTO.Home.City,
                        State = customerDTO.Home.State,
                        Zip = customerDTO.Home.Zip
                    },
                    Office = new Address
                    {
                        Street = customerDTO.Office.Street,
                        City = customerDTO.Office.City,
                        State = customerDTO.Office.State,
                        Zip = customerDTO.Office.Zip
                    },
                    FavoriteColors = customerDTO.FavoriteColors.Select(fc => new FavoriteColor { Color = fc.Color }).ToList(),
                    Age = customerDTO.Age,
                    IsRewarded = customerDTO.IsRewarded,
                    ExternalId = customerDTO.ExternalId
                };

                var selectedCustomer = await _customerRepository.GetCustomerByExternalIdAsync(customer.ExternalId);

                if (selectedCustomer != null)
                {
                    selectedCustomer.IsRewarded = true;
                    await _customerRepository.UpdateCustomerAsync(selectedCustomer);
                }
                else
                {
                    customer.IsRewarded = true;
                    await _customerRepository.AddCustomerAsync(customer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
