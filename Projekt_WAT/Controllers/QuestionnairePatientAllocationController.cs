using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Models.ViewModels;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Controllers
{
    [Authorize(Roles = "Administrator,Doctor")]
    public class QuestionnairePatientAllocationController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAnswerRepo _answerRepo;
        private readonly IPatientQuestionnaireRepo _patientQuestionnaireRepo;

        public QuestionnairePatientAllocationController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IAnswerRepo answerRepo,
            IPatientQuestionnaireRepo patientQuestionnaireRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _answerRepo = answerRepo;
            _patientQuestionnaireRepo = patientQuestionnaireRepo;
        }



        // GET: QuestionnairePatientAllocationController
        public ActionResult Index()
        {
            var patients = _userRepo.GetAll().Result;
            var model = (from patient in patients where string.IsNullOrEmpty(patient.PWZ) && !string.IsNullOrEmpty(patient.PESEL) select new PatientViewModel {EmailAddress = patient.Email, FirstName = patient.FirstName, LastName = patient.LastName, PESEL = patient.PESEL}).ToList();
            return View(model);
        }

        public ActionResult ManagePatientQuestionnaires(string id)
        {

            var patientQuestionnaires = _patientQuestionnaireRepo.GetPatientQuestionnairesByEmail(id).Result;
            var patientQuestionnaireViewModels = patientQuestionnaires.Select(patientQuestionnaire => new PatientQuestionnaireViewModel {PatientEmail = patientQuestionnaire.PatientEmail, QuestionnaireId = patientQuestionnaire.QuestionnaireId}).ToList();

            var questionnaires = _questionnaireRepo.GetAll().Result;
            var questionnaireViewModels = questionnaires.Select(questionnaire => new QuestionnaireViewModel { Fields = null, Id = questionnaire.Id, Name = questionnaire.Name }).ToList();

            foreach (var item in patientQuestionnaireViewModels)
            {
                item.QuestionnaireName = _questionnaireRepo.GetById(item.QuestionnaireId).Result.Name;
                if (questionnaireViewModels.Any(x => x.Id == item.QuestionnaireId))
                    questionnaireViewModels.Remove(
                        questionnaireViewModels.FirstOrDefault(q => q.Id == item.QuestionnaireId));
            }

            var model = new PatientQuestionnaireManagerViewModel
            {
                patientEmail = id,
                PatientQuestionnaires = patientQuestionnaireViewModels,
                Questionnaires = questionnaireViewModels
            };

            return View(model);
        }

        public ActionResult AssignQuestionnaireToPatient(int id, string email)
        {
            _patientQuestionnaireRepo.Create(new UserQuestionnaire
            {
                PatientEmail = email,
                QuestionnaireId = id
            }).Wait();

            return RedirectToAction("ManagePatientQuestionnaires", new { id = email });
        }

        public ActionResult RemoveQuestionnaireFromPatient(int id, string email)
        {
            var patientQuestionnaire = _patientQuestionnaireRepo.GetByIdAndUserEmail(id, email).Result;
            _patientQuestionnaireRepo.Delete(patientQuestionnaire).Wait();

            return RedirectToAction("ManagePatientQuestionnaires", new { id = email });
        }

        // GET: QuestionnairePatientAllocationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuestionnairePatientAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionnairePatientAllocationController/Create
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

        // GET: QuestionnairePatientAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionnairePatientAllocationController/Edit/5
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

        // GET: QuestionnairePatientAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionnairePatientAllocationController/Delete/5
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
