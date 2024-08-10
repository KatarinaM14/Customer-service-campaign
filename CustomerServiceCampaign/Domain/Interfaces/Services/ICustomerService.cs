using Domain.Models.BaseModels;
using SharedProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task RewardCustomerAsync(CustomerDTO customerDTO);
        Task DeleteCustomerAsync(int id);
    }
}
