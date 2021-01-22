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
    public class QuestionRepo : IQuestionRepo
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Question entity)
        {
            await _context.Questions.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Question entity)
        {
            _context.Questions.Remove(entity);
            return await Save();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Question>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Question> GetById(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
            return question;
        }

        public async Task<ICollection<Question>> GetQuestionsByQuestionnaireId(int id)
        {
            var questions = await _context.Questions.Where(q => q.QuestionnaireId == id).ToListAsync();
            return questions;
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> Update(Question entity)
        {
            throw new NotImplementedException();
        }
    }
}
