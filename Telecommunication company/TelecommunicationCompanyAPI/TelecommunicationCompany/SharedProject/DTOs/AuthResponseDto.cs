using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedProject.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
