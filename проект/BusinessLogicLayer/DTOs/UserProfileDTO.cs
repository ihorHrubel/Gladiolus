using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class UserProfileDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Gender { get; set; }
        public bool IsOnline { get; set; }
        public string CurrentLocation { get; set; }
        public DateTime LastTimeOnline { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
