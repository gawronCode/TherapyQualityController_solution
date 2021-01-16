using System.Collections.Generic;
using System.Threading.Tasks;
using TherapyQualityController.Models;
using TherapyQualityController.Models.DbModels;

namespace TherapyQualityController.Repositories.IRepos
{
    public interface IQuestionRepo : IGeneralRepo<Question>
    {
        Task<ICollection<Question>> GetQuestionsByQuestionnaireId(int id);
    }
}
