using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Repositories.Repos;

namespace TherapyQualityController.Repositories.IRepos
{
    public interface IUserQuestionnaireAnswerRepo : IGeneralRepo<UserQuestionnaireAnswer>
    {
        Task<ICollection<UserQuestionnaireAnswer>> GetByUserEmailAndQuestionnaireId(string email, int id);
        Task<ICollection<UserQuestionnaireAnswer>> GetByUserEmail(string email);
    }
}
