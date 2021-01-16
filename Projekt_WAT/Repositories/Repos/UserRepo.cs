using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TherapyQualityController.Data;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Repositories.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetPatientQuestionnaireIdByEmail(string email)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(q => q.Email == email);
            int questionnaireId = patient.QuestionnaireId ?? default(int);
            return questionnaireId;
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
