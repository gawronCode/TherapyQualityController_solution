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
    public class UserAnswerRepo : IUserAnswerRepo
    {
        private readonly ApplicationDbContext _context;

        public UserAnswerRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(UserAnswer entity)
        {
            await _context.UserAnswers.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(UserAnswer entity)
        {
            _context.UserAnswers.Remove(entity);
            return await Save();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserAnswer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserAnswer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<UserAnswer>> GetUserAnswersByUserEmail(string email)
        {
            var userAnswers = await _context.UserAnswers.Where(q => q.UserEmail == email).ToListAsync();
            return userAnswers;
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> Update(UserAnswer entity)
        {
            throw new NotImplementedException();
        }
    }
}
