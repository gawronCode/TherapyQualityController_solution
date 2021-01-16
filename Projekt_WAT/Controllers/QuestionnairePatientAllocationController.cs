using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyQualityController.Models.ViewModels;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Controllers
{
    public class QuestionnairePatientAllocationController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAnswerRepo _answerRepo;

        public QuestionnairePatientAllocationController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IAnswerRepo answerRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _answerRepo = answerRepo;
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

            return View();
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
