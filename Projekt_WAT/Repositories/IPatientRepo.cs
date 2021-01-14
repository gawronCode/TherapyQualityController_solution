using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Repositories
{
    public interface IPatientRepo
    {
        Task<int> GetPatientQuestionnaireIdByEmail(string email);
    }
}
