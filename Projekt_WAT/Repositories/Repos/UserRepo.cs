using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ICollection<int>> GetUserQuestionnairesIdByEmail(string email)
        {
            var patient = await _context.AppUsers.FirstOrDefaultAsync(q => q.Email == email);
            var userQuestionnaires = _context.UserQuestionnaires.Where(q => q.PatientEmail == email);
            var questionnairesId = new List<int>();

            foreach (var userQuestionnaire in userQuestionnaires)
            {
                questionnairesId.Add(userQuestionnaire.QuestionnaireId);
            }
            

            
            return questionnairesId;
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
