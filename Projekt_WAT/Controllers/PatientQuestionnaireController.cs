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
            int i = 0;
            foreach (var question in questions)
            {
                var questionVm = new QuestionViewModel
                {
                    Contents = question.Contents
                };
                model.Fields.Add(new FieldViewModel
                {
                    Count = i,
                    Answer = new AnswerViewModel(),
                    Question = questionVm
                });
                i++;
            }

            return View(model);
        }

        // GET: PatientQuestionnaireController/Create
        public ActionResult SendAnswers()
        {
            return View();
        }

        // POST: PatientQuestionnaireController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendAnswers(IFormCollection collection)
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
