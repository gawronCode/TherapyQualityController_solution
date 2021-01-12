using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models;

namespace TherapyQualityController.Repositories
{
    public class QuestionnaireRepo : IQuestionnaireRepo
    {
        public Task<bool> Create(Questionnaire entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Questionnaire entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Questionnaire>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Questionnaire> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Questionnaire entity)
        {
            throw new NotImplementedException();
        }
    }
}
