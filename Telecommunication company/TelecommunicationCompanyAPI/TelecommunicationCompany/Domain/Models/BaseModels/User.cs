using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BaseModels
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool IsLoggedIn { get; set; }

        public List<Reward> RewardsIssued { get; set; } = new List<Reward>();
    }
}
