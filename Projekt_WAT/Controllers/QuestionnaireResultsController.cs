using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IAnswerRepo _answerRepo;
        private readonly IPatientQuestionnaireRepo _patientQuestionnaireRepo;
        private readonly UserManager<User> _userManager;
        private readonly IUserAnswerRepo _userAnswerRepo;
        private readonly IUserQuestionnaireAnswerRepo _userQuestionnaireAnswerRepo;

        public QuestionnaireResultsController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IAnswerRepo answerRepo,
            IPatientQuestionnaireRepo patientQuestionnaireRepo,
            UserManager<User> userManager,
            IUserAnswerRepo userAnswerRepo,
            IUserQuestionnaireAnswerRepo userQuestionnaireAnswerRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _answerRepo = answerRepo;
            _patientQuestionnaireRepo = patientQuestionnaireRepo;
            _userManager = userManager;
            _userAnswerRepo = userAnswerRepo;
            _userQuestionnaireAnswerRepo = userQuestionnaireAnswerRepo;
        }

        // GET: QuestionnaireResultsController
        public ActionResult Index()
        {
            var patients = _userRepo.GetAll().Result;
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


        public ActionResult PatientQuestionnaires(string id)
        {
            var patientQuestionnaires = _patientQuestionnaireRepo.GetPatientQuestionnairesByEmail(id).Result;
            var patientQuestionnaireViewModels = patientQuestionnaires.Select(patientQuestionnaire =>
                new PatientQuestionnaireViewModel
                {
                    PatientEmail = id,
                    QuestionnaireId = patientQuestionnaire.QuestionnaireId,
                    QuestionnaireName = _questionnaireRepo.GetById(patientQuestionnaire.QuestionnaireId).Result.Name
                }).ToList();

            var model = new PatientQuestionnaireManagerViewModel
            {
                patientEmail = id,
                PatientQuestionnaires = patientQuestionnaireViewModels
            };

            return View(model);
        }

        public ActionResult PatientQuestionnaireResults(int id, string email)
        {
            var userAnswers = _userAnswerRepo.GetUserAnswersByUserEmail(email).Result;
            
            var answersToSelectedQuestionnaire = userAnswers.Where(answer =>
                _questionRepo.GetById(answer.QuestionId).Result.QuestionnaireId == id).ToList();

            var user = _userManager.FindByEmailAsync(email).Result;

            var model = new ResultViewModel
            {
                NumberOfSolvedQuestionnaires = 0,
                PatientEmail = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PESEL = user.PESEL
            };

            if (answersToSelectedQuestionnaire.Count==0) return View(model);

            var questionnaireAnswered = _userQuestionnaireAnswerRepo.GetByUserEmailAndQuestionnaireId(email, id).Result;


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
            model.QuestionnaireName = _questionnaireRepo.GetById(id).Result.Name;
            model.StudyStart = questionnaireAnswerDetails.FirstOrDefault().AnswerDate;

            return View(model);
        }


        // GET: QuestionnaireResultsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuestionnaireResultsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionnaireResultsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionnaireResultsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionnaireResultsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionnaireResultsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionnaireResultsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
