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
    public class PatientQuestionnaireRepo : IPatientQuestionnaireRepo
    {

        private readonly ApplicationDbContext _context;

        public PatientQuestionnaireRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(UserQuestionnaire entity)
        {
            await _context.UserQuestionnaires.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(UserQuestionnaire entity)
        {
            _context.UserQuestionnaires.Remove(entity);
            return await Save();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserQuestionnaire>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserQuestionnaire> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserQuestionnaire> GetByIdAndUserEmail(int id, string email)
        {
            var userQuestionnaire =
                await _context.UserQuestionnaires.FirstOrDefaultAsync(q =>
                    q.QuestionnaireId == id && q.PatientEmail == email);
            return userQuestionnaire;
        }

        public async Task<ICollection<UserQuestionnaire>> GetPatientQuestionnairesByEmail(string email)
        {
            var patientQuestionnaires = await _context.UserQuestionnaires.Where(q => q.PatientEmail == email).ToListAsync();
            return patientQuestionnaires;

        }

        public async Task<ICollection<UserQuestionnaire>> GetPatientQuestionnairesByQuestionnaireId(int id)
        {
            var patientsQuestionnaires = await _context.UserQuestionnaires.Where(q => q.QuestionnaireId == id).ToListAsync();
            return patientsQuestionnaires;
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> Update(UserQuestionnaire entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<int>> GetUserQuestionnairesIdByEmail(string email)
        {
            var userQuestionnaires = await _context.UserQuestionnaires.Where(q => q.PatientEmail == email).ToListAsync();
            return userQuestionnaires.Select(userQuestionnaire => userQuestionnaire.QuestionnaireId).ToList();
        }


    }
}
