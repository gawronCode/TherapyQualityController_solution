using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Models.ViewModels
{
    public class PatientQuestionnaireManagerViewModel
    {
        public string patientEmail { get; set; }
        public List<PatientQuestionnaireViewModel> PatientQuestionnaires { get; set; }
        public List<QuestionnaireViewModel> Questionnaires { get; set; }
    }
}
