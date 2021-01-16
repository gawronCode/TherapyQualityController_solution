using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TherapyQualityController.Models.DbModels
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PESEL { get; set; }
        public string PWZ { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        
    }
}
