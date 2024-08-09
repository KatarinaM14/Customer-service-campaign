using Domain.Models.BaseModels;
using SharedProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserDto registerUserDto);
        Task<AuthResponseDto> LoginAsync(UserDto loginUserDto);
        Task LogoutAsync(int userId);
    }
}
