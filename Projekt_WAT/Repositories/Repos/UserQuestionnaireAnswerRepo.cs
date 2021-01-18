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
    public class UserQuestionnaireAnswerRepo : IUserQuestionnaireAnswerRepo
    {

        private readonly ApplicationDbContext _context;

        public UserQuestionnaireAnswerRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(UserQuestionnaireAnswer entity)
        {
            await _context.UserQuestionnaireAnswers.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(UserQuestionnaireAnswer entity)
        {
            _context.UserQuestionnaireAnswers.Remove(entity);
            return await Save();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserQuestionnaireAnswer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserQuestionnaireAnswer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<UserQuestionnaireAnswer>> GetByUserEmail(string email)
        {
            var answeredQuestionnaires =
                await _context.UserQuestionnaireAnswers.Where(q => q.UserEmail == email).ToListAsync();
            return answeredQuestionnaires;
        }

        public async Task<ICollection<UserQuestionnaireAnswer>> GetByUserEmailAndQuestionnaireId(string email, int id)
        {
            var answeredQuestionnaires = await _context.UserQuestionnaireAnswers
                .Where(q => q.UserEmail == email && q.QuestionnaireId == id).ToListAsync();
            return answeredQuestionnaires;
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> Update(UserQuestionnaireAnswer entity)
        {
            throw new NotImplementedException();
        }
    }
}
