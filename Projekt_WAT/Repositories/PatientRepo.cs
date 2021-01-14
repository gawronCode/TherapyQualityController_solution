using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TherapyQualityController.Data;

namespace TherapyQualityController.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly ApplicationDbContext _context;

        public PatientRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetPatientQuestionnaireIdByEmail(string email)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(q => q.Email == email);
            int questionnaireId = patient.QuestionnaireId ?? default(int);
            return questionnaireId;
        }
    }
}
