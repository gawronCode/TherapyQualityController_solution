using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Models.TmpModels;
using TherapyQualityController.Models.ViewModels;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Controllers
{
    [Authorize(Roles = "Administrator,Doctor")]
    public class QuestionnaireResultsController : Controller
    {
        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IPatientQuestionnaireRepo _patientQuestionnaireRepo;
        private readonly UserManager<User> _userManager;
        private readonly IUserAnswerRepo _userAnswerRepo;
        private readonly IUserQuestionnaireAnswerRepo _userQuestionnaireAnswerRepo;

        public QuestionnaireResultsController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IPatientQuestionnaireRepo patientQuestionnaireRepo,
            UserManager<User> userManager,
            IUserAnswerRepo userAnswerRepo,
            IUserQuestionnaireAnswerRepo userQuestionnaireAnswerRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _patientQuestionnaireRepo = patientQuestionnaireRepo;
            _userManager = userManager;
            _userAnswerRepo = userAnswerRepo;
            _userQuestionnaireAnswerRepo = userQuestionnaireAnswerRepo;
        }

        public async Task<ActionResult> Index()
        {
            var patients = await _userRepo.GetAll();
            var model = (from patient in patients
                         where _userManager.IsInRoleAsync(patient, "Patient").Result
                         select new PatientViewModel
                         {
                             EmailAddress = patient.Email,
                             FirstName = patient.FirstName,
                             LastName = patient.LastName,
                             PESEL = patient.PESEL
                         }).ToList();

            return View(model);
        }


        public async Task<ActionResult> PatientQuestionnaires(string id)
        {
            var patientQuestionnaires = await _patientQuestionnaireRepo.GetPatientQuestionnairesByEmail(id);
            var patientQuestionnaireViewModels = patientQuestionnaires.Select(patientQuestionnaire =>
                new PatientQuestionnaireViewModel
                {
                    PatientEmail = id,
                    QuestionnaireId = patientQuestionnaire.QuestionnaireId,
                    QuestionnaireName = _questionnaireRepo.GetById(patientQuestionnaire.QuestionnaireId).Result.Name
                }).ToList();

            var model = new PatientQuestionnaireManagerViewModel
            {
                PatientEmail = id,
                PatientQuestionnaires = patientQuestionnaireViewModels
            };

            return View(model);
        }

        public async Task<ActionResult> PatientQuestionnaireResults(int id, string email)
        {
            var userAnswers = await _userAnswerRepo.GetUserAnswersByUserEmail(email);
            
            var answersToSelectedQuestionnaire = userAnswers.Where(answer =>
                _questionRepo.GetById(answer.QuestionId).Result.QuestionnaireId == id).ToList();

            var user = await _userManager.FindByEmailAsync(email);

            var model = new ResultViewModel
            {
                NumberOfSolvedQuestionnaires = 0,
                PatientEmail = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PESEL = user.PESEL
            };

            if (answersToSelectedQuestionnaire.Count==0) return View(model);

            var questionnaireAnswered = await _userQuestionnaireAnswerRepo.GetByUserEmailAndQuestionnaireId(email, id);
            var questionnaireAnswerDetails = new List<QuestionnaireAnswerDetails>();

            foreach (var questionnaireAnswer in questionnaireAnswered)
            {
                var questionnaireDetails = new QuestionnaireAnswerDetails
                {
                    AnswerDate = questionnaireAnswer.AnswerDate,
                    AnswerCount = 0,
                    AnswerSum = 0
                };
                foreach (var answer in answersToSelectedQuestionnaire.Where(answer => answer.UserQuestionnaireAnswerId == questionnaireAnswer.Id))
                {
                    questionnaireDetails.AnswerCount++;
                    questionnaireDetails.AnswerSum += answer.Value;
                }
                questionnaireAnswerDetails.Add(questionnaireDetails);
            }

            questionnaireAnswerDetails = questionnaireAnswerDetails.Where(q => 
                q.AnswerDate.HasValue).OrderBy(q => 
                q.AnswerDate.Value).ToList();

            var averageScorePerQuestionnaire = questionnaireAnswerDetails.Select(answerDetail => answerDetail.GetAverageScore()).ToList();

            model.AverageQuestionnaireScore = averageScorePerQuestionnaire.ToArray();
            model.LastQuestionnaireDate = questionnaireAnswerDetails.LastOrDefault().AnswerDate;
            model.NumberOfSolvedQuestionnaires = questionnaireAnswerDetails.Count;
            model.QuestionnaireName = (await _questionnaireRepo.GetById(id)).Name;
            model.StudyStart = questionnaireAnswerDetails.FirstOrDefault().AnswerDate;

            return View(model);
        }

    }
}
