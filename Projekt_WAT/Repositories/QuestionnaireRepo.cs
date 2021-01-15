using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TherapyQualityController.Data;
using TherapyQualityController.Models;

namespace TherapyQualityController.Repositories
{
    public class QuestionnaireRepo : IQuestionnaireRepo
    {

        private readonly ApplicationDbContext _context;

        public QuestionnaireRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Questionnaire entity)
        {
            await _context.Questionnaires.AddAsync(entity);
            return await Save();
        }

        public Task<bool> Delete(Questionnaire entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Questionnaire>> GetAll()
        {
            var questionnaires = await _context.Questionnaires.ToListAsync();
            return questionnaires;
        }

        public async Task<Questionnaire> GetById(int id)
        {
            var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(q => q.Id == id);
            return questionnaire;
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> Update(Questionnaire entity)
        {
            throw new NotImplementedException();
        }
    }
}
