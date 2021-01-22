using System.Collections.Generic;

namespace TherapyQualityController.Models.ViewModels
{
    public class PatientQuestionnaireManagerViewModel
    {
        public string PatientEmail { get; set; }
        public List<PatientQuestionnaireViewModel> PatientQuestionnaires { get; set; }
        public List<QuestionnaireViewModel> Questionnaires { get; set; }
    }
}
