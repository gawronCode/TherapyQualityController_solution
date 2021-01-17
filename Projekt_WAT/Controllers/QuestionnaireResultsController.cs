using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TherapyQualityController.Models.DbModels;
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

        public QuestionnaireResultsController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IAnswerRepo answerRepo,
            IPatientQuestionnaireRepo patientQuestionnaireRepo,
            UserManager<User> userManager,
            IUserAnswerRepo userAnswerRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _answerRepo = answerRepo;
            _patientQuestionnaireRepo = patientQuestionnaireRepo;
            _userManager = userManager;
            _userAnswerRepo = userAnswerRepo;
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
            // var userAnswers = _userAnswerRepo.GetUserAnswersByUserEmail(id).Result;
            // var questions = userAnswers.Select(answer => _questionRepo.GetById(answer.QuestionId).Result).ToList();
            // questions = questions.Distinct().ToList();
            // var questionnaires = questions.Select(question => _questionnaireRepo.GetById(question.QuestionnaireId).Result).ToList();
            // questionnaires = questionnaires.Distinct().ToList();

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
            
            // var questions = userAnswers.Select(answer => _questionRepo.GetById(answer.QuestionId).Result).ToList();
            // questions = questions.Distinct().ToList();
            //
            // var questionnaires = questions.Select(question => _questionnaireRepo.GetById(question.QuestionnaireId).Result).ToList();
            // questionnaires = questionnaires.Distinct().ToList();

            var answersToSelectedQuestionnaire = userAnswers.Where(answer => 
                _questionRepo.GetById(answer.QuestionId).Result.QuestionnaireId == id).ToList();



            return RedirectToAction(nameof(Index));
            // return View();
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
