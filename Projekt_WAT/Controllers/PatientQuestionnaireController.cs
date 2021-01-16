using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TherapyQualityController.Models;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Models.ViewModels;
using TherapyQualityController.Repositories;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Controllers
{
    [Authorize]
    public class PatientQuestionnaireController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IUserRepo _userRepo;

        public PatientQuestionnaireController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;

        }

        // GET: PatientQuestionnaireController
        public ActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userQuestionnaireId = _userRepo.GetUserQuestionnairesIdByEmail(userEmail).Result;

            var questionnaires = userQuestionnaireId.Select(id => _questionnaireRepo.GetById(id).Result).ToList();

            var model = questionnaires.Select(questionnaire => new QuestionnaireViewModel {Fields = null, Id = questionnaire.Id, Name = questionnaire.Name}).ToList();

            return View(model);
        }

        public ActionResult FillQuestionnaire(int id)
        {

            var questionnaire = _questionnaireRepo.GetById(id).Result;
            var questions = _questionRepo.GetQuestionsByQuestionnaireId(id).Result;


            var model = new QuestionnaireViewModel
            {
                Name = questionnaire.Name,
                Fields = new List<FieldViewModel>()
            };
            var i = 0;
            foreach (var question in questions)
            {
                model.Fields.Add(new FieldViewModel
                {
                    Count = i,
                    Question = question.Contents,
                    QuestionId = question.Id
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

            var count = Convert.ToInt32(form["count"]);
            var answers = new List<UserAnswer>();

            for (var i = 0; i < count; i++)
            {
                string data = form[$"opt{i.ToString()}"];
                var dataParsed = data.Split(',');
                answers.Add(new UserAnswer
                {
                    AnswerDate = DateTime.Now,
                    QuestionId = Convert.ToInt32(dataParsed[0]),
                    Value = Convert.ToInt32(dataParsed[1])

                });
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

    }
}
