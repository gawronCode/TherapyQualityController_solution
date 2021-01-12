using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models;

namespace TherapyQualityController.Repositories
{
    public class QuestionRepo : IQuestionRepo
    {
        public Task<bool> Create(Question entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Question entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Question>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Question> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Question entity)
        {
            throw new NotImplementedException();
        }
    }
}
