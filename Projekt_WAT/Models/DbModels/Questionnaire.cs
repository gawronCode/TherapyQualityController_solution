using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyQualityController.Models.DbModels
{
    public class Questionnaire
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? CreationDate { get; set; }
        
    }
}
