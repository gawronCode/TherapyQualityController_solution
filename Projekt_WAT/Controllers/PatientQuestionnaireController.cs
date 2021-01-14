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
                var questionVm = question.Contents;
                model.Fields.Add(new FieldViewModel
                {
                    Count = i,
                    Question = questionVm
                });
                i++;
            }

            return View(model);
        }

        // GET: PatientQuestionnaireController/Create
        public ActionResult SendAnswers()
        {
            return RedirectToAction(nameof(Index));
        }

        // POST: PatientQuestionnaireController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendAnswers(IFormCollection form)
        {

            int count = Convert.ToInt32(form["count"]);
            List<int> answers = new List<int>();

            for (int i = 0; i < count; i++)
            {
                answers.Add(Convert.ToInt32(form[$"opt{i.ToString()}"]));
            }

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // new FieldViewModel{
        //     Count = item.Count,
        //     Question = item.Question,
        //     Answer = 1}
}
}
