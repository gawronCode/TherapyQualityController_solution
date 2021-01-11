using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TherapyQualityController.Models
{
    public class Patient : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PESEL { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
