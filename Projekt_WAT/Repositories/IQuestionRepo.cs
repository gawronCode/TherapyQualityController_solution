using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models;

namespace TherapyQualityController.Repositories
{
    public interface IQuestionRepo : IGeneralRepo<Question>
    {
        Task<ICollection<Question>> GetQuestionsByQuestionnaireId(int id);
    }
}
