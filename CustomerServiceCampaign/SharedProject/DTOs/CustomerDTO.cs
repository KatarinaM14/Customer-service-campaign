using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedProject.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int HomeId { get; set; }
        public AddressDTO Home { get; set; }
        public int OfficeId { get; set; }
        public AddressDTO Office { get; set; }
        public List<FavoriteColorDTO> FavoriteColors { get; set; }
        public int Age { get; set; }
        public bool IsRewarded { get; set; }
        public int ExternalId { get; set; }
        public bool AddedInMerge { get; set; }
    }
}
