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
    [Authorize(Roles = "Patient, Administrator")]
    public class PatientQuestionnaireController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAnswerRepo _answerRepo;
        private readonly IUserAnswerRepo _userAnswerRepo;
        private readonly IUserQuestionnaireAnswerRepo _userQuestionnaireAnswerRepo;

        public PatientQuestionnaireController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IAnswerRepo answerRepo,
            IUserAnswerRepo userAnswerRepo,
            IUserQuestionnaireAnswerRepo userQuestionnaireAnswerRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _answerRepo = answerRepo;
            _userAnswerRepo = userAnswerRepo;
            _userQuestionnaireAnswerRepo = userQuestionnaireAnswerRepo;
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
                var answers = _answerRepo.GetAnswersByQuestionId(question.Id).Result;
                var answerViewModels = answers.Select(answer => new AnswerViewModel { Content = answer.Content, Value = answer.Value }).ToList();

                model.Fields.Add(new FieldViewModel
                {
                    Count = i,
                    Question = question.Contents,
                    QuestionId = question.Id,
                    Answers = answerViewModels
                });
                i++;
            }

            return View(model);

        }

        
        public ActionResult SendAnswers(IFormCollection form)
        {

            var count = Convert.ToInt32(form["count"]);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var answers = new List<UserAnswer>();

            for (var i = 0; i < count; i++)
            {
                string data = form[$"opt{i.ToString()}"];
                if(string.IsNullOrEmpty(data)) return RedirectToAction(nameof(ErrorInfo));
                var dataParsed = data.Split(',');
                answers.Add(new UserAnswer
                {
                    QuestionId = Convert.ToInt32(dataParsed[0]),
                    Value = Convert.ToInt32(dataParsed[1]),
                    UserEmail = userEmail
                });
            }

            var userQuestionnaireAnswer = new UserQuestionnaireAnswer
            {
                AnswerDate = DateTime.Now,
                QuestionnaireId = _questionRepo.GetById(answers[0].QuestionId).Result.QuestionnaireId,
                UserEmail = answers[0].UserEmail
            };

            _userQuestionnaireAnswerRepo.Create(userQuestionnaireAnswer).Wait();

            foreach (var answer in answers)
            {
                answer.UserQuestionnaireAnswerId = userQuestionnaireAnswer.Id;
            }


            foreach (var answer in answers)
            {
                _userAnswerRepo.Create(answer).Wait();
            }

            return RedirectToAction(nameof(SuccessInfo));
            
        }

        public ActionResult SuccessInfo()
        {
            return View();
        }

        public ActionResult ErrorInfo()
        {
            return View();
        }

    }
}
