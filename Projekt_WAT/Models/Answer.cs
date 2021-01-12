using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Models
{
    public class Answer
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int Range { get; set; }
        [Required]
        public DateTime? AnswerDate { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
