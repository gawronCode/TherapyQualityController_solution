namespace TherapyQualityController.Models.DbModels
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Value { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
