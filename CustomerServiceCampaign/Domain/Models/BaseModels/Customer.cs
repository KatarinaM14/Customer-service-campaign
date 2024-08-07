using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BaseModels
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int HomeId { get; set; }
        public Address Home { get; set; }
        public int OfficeId { get; set; }
        public Address Office { get; set; }     
        public List<FavoriteColor> FavoriteColors { get; set; }
        public int Age { get; set; }
        public bool IsRewarded { get; set; }
        public int ExternalId { get; set; }
    }
}
