using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int Range { get; set; }
        public DateTime? AnswerDate { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
