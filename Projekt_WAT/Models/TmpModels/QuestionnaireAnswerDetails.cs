using System;

namespace TherapyQualityController.Models.TmpModels
{
    public class QuestionnaireAnswerDetails
    {
        public DateTime? AnswerDate { get; set; }
        public int AnswerCount { get; set; }
        public int AnswerSum { get; set; }

        public double GetAverageScore()
        {
            return (double) AnswerSum / (double) AnswerCount;
        }
    }
}
