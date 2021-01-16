using System.Collections.Generic;
using System.Threading.Tasks;
using TherapyQualityController.Models;
using TherapyQualityController.Models.DbModels;

namespace TherapyQualityController.Repositories.IRepos
{
    public interface IUserRepo : IGeneralRepo<User>
    {
        Task<ICollection<int>> GetUserQuestionnairesIdByEmail(string email);
    }
}
