using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models.DbModels;

namespace TherapyQualityController.Repositories.IRepos
{
    public interface IPatientQuestionnaireRepo : IGeneralRepo<UserQuestionnaire>
    {
        Task<ICollection<UserQuestionnaire>> GetPatientQuestionnairesByEmail(string email);
        Task<ICollection<UserQuestionnaire>> GetPatientQuestionnairesByQuestionnaireId(int id);
        Task<UserQuestionnaire> GetByIdAndUserEmail(int id, string email);
    }
}
