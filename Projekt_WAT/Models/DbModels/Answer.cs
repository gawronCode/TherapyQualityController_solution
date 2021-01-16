using System;
using System.ComponentModel.DataAnnotations;

namespace TherapyQualityController.Models.DbModels
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
