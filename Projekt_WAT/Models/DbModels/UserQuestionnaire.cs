using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
