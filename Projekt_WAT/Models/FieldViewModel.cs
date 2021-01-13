using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Models
{
    public class FieldViewModel
    {
        public int Count { get; set; }
        public QuestionViewModel Question { get; set; }
        public AnswerViewModel Answer { get; set; }
    }
}
