using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TherapyQualityController.Models;
using TherapyQualityController.Repositories;

namespace TherapyQualityController.Controllers
{
    public class PatientQuestionnaireController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly UserManager<User> _userManager;

        public PatientQuestionnaireController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;

        }

        // GET: PatientQuestionnaireController
        public ActionResult Index()
        {
            // var userEmail = User.FindFirstValue(ClaimTypes.Email);
            // var user = _userManager.FindByEmailAsync(userEmail).Result;
            // var userQuestionnaireId = user.QuestionnaireId;

            var questionnaire = _questionnaireRepo.GetById(2).Result;
            var questions = _questionRepo.GetQuestionsByQuestionnaireId(2).Result;

            var model = new QuestionnaireViewModel
            {
                Name = questionnaire.Name,
                Fields = new List<FieldViewModel>()
            };

            foreach (var question in questions)
            {
                var questionVm = new QuestionViewModel
                {
                    Contents = question.Contents
                };
                model.Fields.Add(new FieldViewModel
                {
                    Answer = new AnswerViewModel(),
                    Question = questionVm
                });
            }

            return View(model);
        }

        // GET: PatientQuestionnaireController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatientQuestionnaireController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientQuestionnaireController/Create
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

        // GET: PatientQuestionnaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientQuestionnaireController/Edit/5
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

        // GET: PatientQuestionnaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientQuestionnaireController/Delete/5
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
