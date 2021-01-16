using System.ComponentModel.DataAnnotations;

namespace TherapyQualityController.Models.DbModels
{
    public class Question
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Contents { get; set; }
        [Required]
        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

    }
}
