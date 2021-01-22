using System.Collections.Generic;
using System.Threading.Tasks;
using TherapyQualityController.Models.DbModels;

namespace TherapyQualityController.Repositories.IRepos
{
    public interface IUserQuestionnaireAnswerRepo : IGeneralRepo<UserQuestionnaireAnswer>
    {
        Task<ICollection<UserQuestionnaireAnswer>> GetByUserEmailAndQuestionnaireId(string email, int id);
        Task<ICollection<UserQuestionnaireAnswer>> GetByUserEmail(string email);
    }
}
