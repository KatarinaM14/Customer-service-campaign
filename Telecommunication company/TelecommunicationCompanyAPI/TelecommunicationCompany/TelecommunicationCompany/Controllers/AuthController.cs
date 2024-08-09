using Domain.Interfaces.Services;
using Domain.Models.BaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedProject.DTOs;
using System.Security.Claims;

namespace TelecommunicationCompany.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;

        public AuthController(IAuthService authService, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _currentUserService = currentUserService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] UserDto registerUserDto)
        {
            try
            {
                if (registerUserDto.FirstName == null || registerUserDto.LastName == null || registerUserDto.Username == null || registerUserDto.Password == null)
                {
                    return BadRequest("Invalid data!");
                }

                var authResponse = await _authService.RegisterAsync(registerUserDto);

                if (authResponse == null)
                    return BadRequest("Registration error.");

                return Ok(authResponse);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult> LogIn([FromBody] UserDto logInUserDto)
        {
            try
            {
                if (logInUserDto.Username == null || logInUserDto.Password == null)
                {
                    return BadRequest("Invalid data!");
                }

                var authResponse = await _authService.LoginAsync(logInUserDto);

                if (authResponse == null)
                    return BadRequest("LogIn error.");

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("LogOut")]
        [Authorize(Roles = "Administrator,User")]
        public async Task<ActionResult> LogOut()
        {
            try
            {
                var userId = _currentUserService.GetCurrentUserId();
                await _authService.LogoutAsync(userId);

                return Ok(new { message = "User logged out successfully." });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
