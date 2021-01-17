using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public QuestionnaireResultsController(IQuestionnaireRepo questionnaireRepo,
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

        // GET: QuestionnaireResultsController
        public ActionResult Index()
        {
            return View();
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
