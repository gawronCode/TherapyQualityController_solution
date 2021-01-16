using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TherapyQualityController.Models;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Models.ViewModels;
using TherapyQualityController.Repositories;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Controllers
{
    [Authorize(Roles = "Administrator,Doctor")]
    public class QuestionnaireManagerController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAnswerRepo _answerRepo;

        public QuestionnaireManagerController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IAnswerRepo answerRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _answerRepo = answerRepo;
        }

        
        public ActionResult Index()
        {
            var questionnaires = _questionnaireRepo.GetAll().Result;
            var model = new List<QuestionnaireViewModel>();

            foreach (var questionnaire in questionnaires)
            {
                model.Add(new QuestionnaireViewModel
                {
                    Fields = null,
                    Id=questionnaire.Id,
                    Name = questionnaire.Name
                });
            }

            return View(model);
        }

        public ActionResult ManageQuestions(int id)
        {
            
            var questionnaire = _questionnaireRepo.GetById(id).Result;
            var questions = _questionRepo.GetQuestionsByQuestionnaireId(id).Result;

            var model = new QuestionnaireViewModel
            {
                Id = id,
                Name = questionnaire.Name,
                Fields = new List<FieldViewModel>()
            };
            var i = 0;
            foreach (var question in questions)
            {
                var answers = _answerRepo.GetAnswersByQuestionId(question.Id).Result;
                var answerViewModels = answers.Select(answer => new AnswerViewModel {Content = answer.Content, Value = answer.Value}).ToList();

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
        
        
        public ActionResult CreateQuestionnaire(string questionnaireName)
        {

            if (questionnaireName == string.Empty || questionnaireName is null) return RedirectToAction(nameof(Index));

            var newQuestionnaire = new Questionnaire
            {
                CreationDate = DateTime.Now,
                Name = questionnaireName
            };

            _questionnaireRepo.Create(newQuestionnaire).Wait();
            return RedirectToAction(nameof(Index));
        }

        //Todo dokończyć to xD Zmiana nazwy ankiety
        // public ActionResult RenameQuestionnaire(int id, string newName)
        // {
        //     var questionnaire = _questionnaireRepo.GetById(id).Result;
        //     questionnaire.Name = newName;
        //     _questionnaireRepo.Update(questionnaire).Wait();
        //     return RedirectToAction(nameof(Index));
        // }

        public ActionResult RemoveQuestionnaire(int id)
        {
            var questions = _questionRepo.GetQuestionsByQuestionnaireId(id).Result;
            

            foreach (var question in questions)
            {
                var answers = _answerRepo.GetAnswersByQuestionId(question.Id).Result;
                foreach (var answer in answers)
                {
                    _answerRepo.Delete(answer).Wait();
                }

                _questionRepo.Delete(question).Wait();
            }

            var questionnaire = _questionnaireRepo.GetById(id).Result;
            _questionnaireRepo.Delete(questionnaire).Wait();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AddQuestionToQuestionnaire(string questionContent, int questionnaireId,
                                                        string answer1, int val1,
                                                        string answer2, int val2,
                                                        string answer3, int val3,
                                                        string answer4, int val4,
                                                        string answer5, int val5)
        {
            
            if (questionContent == string.Empty || questionContent is null) return RedirectToAction("ErrorInfo", new { id = questionnaireId });
            if (answer1 == string.Empty || answer1 is null) return RedirectToAction("ErrorInfo", new { id = questionnaireId });
            if (answer2 == string.Empty || answer2 is null) return RedirectToAction("ErrorInfo", new { id = questionnaireId });
            if (answer3 == string.Empty || answer3 is null) return RedirectToAction("ErrorInfo", new { id = questionnaireId });
            if (answer4 == string.Empty || answer4 is null) return RedirectToAction("ErrorInfo", new { id = questionnaireId });
            if (answer5 == string.Empty || answer5 is null) return RedirectToAction("ErrorInfo", new { id = questionnaireId });

            if(val1 ==0 || val2 == 0 || val3 == 0 || val4 == 0 || val5 == 0) return RedirectToAction("ErrorInfo", new { id = questionnaireId });
            
            var newQuestion = new Question
            {
                Contents = questionContent,
                QuestionnaireId = questionnaireId
            };
            
            _questionRepo.Create(newQuestion).Wait();

            _answerRepo.Create(new Answer
            {
                Content = answer1,
                Value = val1,
                QuestionId = newQuestion.Id
            }).Wait();
            _answerRepo.Create(new Answer
            {
                Content = answer2,
                Value = val2,
                QuestionId = newQuestion.Id
            }).Wait();
            _answerRepo.Create(new Answer
            {
                Content = answer3,
                Value = val3,
                QuestionId = newQuestion.Id
            }).Wait();
            _answerRepo.Create(new Answer
            {
                Content = answer4,
                Value = val4,
                QuestionId = newQuestion.Id
            }).Wait();
            _answerRepo.Create(new Answer
            {
                Content = answer5,
                Value = val5,
                QuestionId = newQuestion.Id
            }).Wait();

            return RedirectToAction("ManageQuestions", new{id=questionnaireId});
        }

        public ActionResult ErrorInfo(int id)
        {
            return View(id);
        }

        public ActionResult RemoveQuestionFromQuestionnaire(int id)
        {
            var question = _questionRepo.GetById(id).Result;
            var questionnaireId = question.QuestionnaireId;
            var answers = _answerRepo.GetAnswersByQuestionId(question.Id).Result;
            foreach (var answer in answers)
            {
                _answerRepo.Delete(answer).Wait();
            }
            _questionRepo.Delete(question).Wait();
            return RedirectToAction("ManageQuestions", new { id = questionnaireId });
        }


        // GET: QuestionnaireManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestionnaireManagerController/Edit/5
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

        // GET: QuestionnaireManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionnaireManagerController/Delete/5
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
