using System.ComponentModel.DataAnnotations;

namespace TherapyQualityController.Models.DbModels
{
    public class UserQuestionnaire
    {
        [Key]
        public int Id { get; set; }

        public string PatientEmail { get; set; }

        public int QuestionnaireId { get; set; }

    }
}
