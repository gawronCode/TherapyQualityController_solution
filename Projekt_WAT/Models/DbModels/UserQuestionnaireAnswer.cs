using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Models.DbModels
{
    public class UserQuestionnaireAnswer
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public DateTime? AnswerDate { get; set; }
    }
}
