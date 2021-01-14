using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TherapyQualityController.Models
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
        public int? QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

    }
}
