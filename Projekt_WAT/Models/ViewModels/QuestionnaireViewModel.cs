using System.Collections.Generic;

namespace TherapyQualityController.Models.ViewModels
{
    public class QuestionnaireViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FieldViewModel> Fields { get; set; }
    }
}
