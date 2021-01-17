using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models.DbModels;

namespace TherapyQualityController.Repositories.IRepos
{
    public interface IUserAnswerRepo : IGeneralRepo<UserAnswer>
    {
        public Task<ICollection<UserAnswer>> GetUserAnswersByUserEmail(string email);
    }
}
