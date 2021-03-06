﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Models.ViewModels;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Controllers
{
    [Authorize(Roles = "Patient, Administrator")]
    public class PatientQuestionnaireController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IAnswerRepo _answerRepo;
        private readonly IUserAnswerRepo _userAnswerRepo;
        private readonly IUserQuestionnaireAnswerRepo _userQuestionnaireAnswerRepo;
        private readonly IPatientQuestionnaireRepo _patientQuestionnaireRepo;

        public PatientQuestionnaireController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IAnswerRepo answerRepo,
            IUserAnswerRepo userAnswerRepo,
            IUserQuestionnaireAnswerRepo userQuestionnaireAnswerRepo,
            IPatientQuestionnaireRepo patientQuestionnaireRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
            _userAnswerRepo = userAnswerRepo;
            _userQuestionnaireAnswerRepo = userQuestionnaireAnswerRepo;
            _patientQuestionnaireRepo = patientQuestionnaireRepo;
        }


        public async Task<ActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userQuestionnaireId = await _patientQuestionnaireRepo.GetUserQuestionnairesIdByEmail(userEmail);
            var questionnaires = userQuestionnaireId.Select(id => _questionnaireRepo.GetById(id).Result).ToList();
            var answeredQuestionnaires = await _userQuestionnaireAnswerRepo.GetByUserEmail(userEmail);
            var todayAnsweredQuestionnaires = answeredQuestionnaires.Where(q => q.AnswerDate.Value.Date == DateTime.Today).ToList();
            var idToRemove = questionnaires.Where(questionnaire => todayAnsweredQuestionnaires.Count(q => q.QuestionnaireId == questionnaire.Id) >= 5).Select(questionnaire => questionnaire.Id).ToList();

            foreach (var id in idToRemove)
            {
                questionnaires.Remove(questionnaires.FirstOrDefault(q => q.Id == id));
            }

            var model = questionnaires.Select(questionnaire => 
                new QuestionnaireViewModel
                {
                    Fields = null,
                    Id = questionnaire.Id,
                    Name = questionnaire.Name
                }).ToList();

            return View(model);
        }


        public async Task<ActionResult> FillQuestionnaire(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var patientQuestionnairesIds = await _patientQuestionnaireRepo.GetUserQuestionnairesIdByEmail(userEmail);
            if (!patientQuestionnairesIds.Contains(id)) return RedirectToAction(nameof(Index));

            var answeredQuestionnaires = await _userQuestionnaireAnswerRepo.GetByUserEmail(userEmail);
            if (answeredQuestionnaires.Where(q => q.AnswerDate.Value.Date == DateTime.Today && q.QuestionnaireId == id).ToList().Count >= 5) return RedirectToAction(nameof(Index));

            var questionnaire = await _questionnaireRepo.GetById(id);
            var questions = await _questionRepo.GetQuestionsByQuestionnaireId(id);
            
            var model = new QuestionnaireViewModel
            {
                Name = questionnaire.Name,
                Fields = new List<FieldViewModel>()
            };

            var i = 0;
            foreach (var question in questions)
            {
                var answers = await _answerRepo.GetAnswersByQuestionId(question.Id);
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

        
        public async Task<ActionResult> SendAnswers(IFormCollection form)
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

            await _userQuestionnaireAnswerRepo.Create(userQuestionnaireAnswer);

            foreach (var answer in answers)
            {
                answer.UserQuestionnaireAnswerId = userQuestionnaireAnswer.Id;
            }

            foreach (var answer in answers)
            {
                await _userAnswerRepo.Create(answer);
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
