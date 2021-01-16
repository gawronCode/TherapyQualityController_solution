using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models.DbModels;

namespace TherapyQualityController.Repositories.IRepos
{
    public interface IAnswerRepo : IGeneralRepo<Answer>
    {
        public Task<ICollection<Answer>> GetAnswersByQuestionId(int id);
    }
}
