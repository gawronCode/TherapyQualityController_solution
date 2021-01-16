using System.Collections.Generic;

namespace TherapyQualityController.Models.ViewModels
{
    public class FieldViewModel
    {
        public int Count { get; set; }
        public string Question { get; set; }
        public int QuestionId { get; set; }
        public List<AnswerViewModel> Answers { get; set; }

    }
}
