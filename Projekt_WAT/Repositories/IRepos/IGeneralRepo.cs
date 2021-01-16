using System.Collections.Generic;
using System.Threading.Tasks;

namespace TherapyQualityController.Repositories.IRepos
{
    public interface IGeneralRepo<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Exists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
