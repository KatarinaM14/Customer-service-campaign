using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            try
            {
                var userId = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userId == null)
                {
                    throw new UnauthorizedAccessException("User ID not found.");
                }

                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
