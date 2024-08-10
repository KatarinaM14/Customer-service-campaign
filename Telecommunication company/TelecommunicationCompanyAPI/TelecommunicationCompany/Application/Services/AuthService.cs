using Application.DTOs;
using Azure;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.BaseModels;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedProject.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
       
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(UserDto registerUserDto)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByUsernameAsync(registerUserDto.Username);

                if (user != null)
                {
                    throw new ApplicationException("Username is already in use.");
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password);

                var userRole = "User";

                var registerUser = new User
                {
                    FirstName = registerUserDto.FirstName,
                    LastName = registerUserDto.LastName,
                    Username = registerUserDto.Username,
                    Password = hashedPassword,
                    IsLoggedIn = true,
                    Role = userRole
                };

                await _unitOfWork.Users.AddUserAsync(registerUser);
                await _unitOfWork.SaveChangesAsync();

                var token = GenerateToken(registerUser);

                var authResponse = new AuthResponseDto
                {
                    Token = token,
                    User = new UserDto
                    {
                        Id = registerUser.Id,
                        FirstName = registerUser.FirstName,
                        LastName = registerUser.LastName,
                        Username = registerUser.Username,
                        Role = registerUser.Role,
                        IsLoggedIn = registerUser.IsLoggedIn
                    }
                };

                return authResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<AuthResponseDto> LoginAsync(UserDto loginUserDto)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByUsernameAsync(loginUserDto.Username);

                if (user == null)
                {
                    throw new ApplicationException("User not found.");
                }

                var validPassword = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Password);

                if (!validPassword)
                {
                    throw new ApplicationException("Invalid password.");
                }

                user.IsLoggedIn = true;
                await _unitOfWork.Users.UpdateUserAsync(user);
                await _unitOfWork.SaveChangesAsync();

                var token = GenerateToken(user);

                var authResponse = new AuthResponseDto
                {
                    Token = token,
                    User = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username,
                        Role = user.Role,
                        IsLoggedIn = user.IsLoggedIn
                    }
                };

                return authResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task LogoutAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
                if (user == null)
                {
                    throw new ApplicationException("User not found.");
                }

                user.IsLoggedIn = false;
                await _unitOfWork.Users.UpdateUserAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var authClaims = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Username),
                        new Claim(ClaimTypes.GivenName, user.FirstName),
                        new Claim(ClaimTypes.Surname, user.LastName),
                        new Claim(ClaimTypes.Role, user.Role)
                });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Subject = authClaims,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
