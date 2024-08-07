using Domain.Models.BaseModels;
using SharedProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ExternalServices
{
    public interface IUserExternalService
    {
        Task<CustomerDTO> GetUserByIdAsync(int id);
    }
}
