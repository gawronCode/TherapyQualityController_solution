using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Models.ViewModels
{
    public class PatientQuestionnaireViewModel
    {
        public string PatientEmail { get; set; }
        public int QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }
    }
}
