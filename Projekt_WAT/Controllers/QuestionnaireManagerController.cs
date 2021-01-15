using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TherapyQualityController.Models;
using TherapyQualityController.Repositories;

namespace TherapyQualityController.Controllers
{
    [Authorize(Roles = "Administrator,Doctor")]
    public class QuestionnaireManagerController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IPatientRepo _patientRepo;

        public QuestionnaireManagerController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IPatientRepo patientRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _patientRepo = patientRepo;

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
        
        
        public ActionResult CreateQuestionnaire(string questionnaireName)
        {

            if (questionnaireName == string.Empty || questionnaireName is null) return RedirectToAction(nameof(Index));

            var newQuestionnaire = new Questionnaire
            {
                CreationDate = DateTime.Now,
                Name = questionnaireName
            };

            _questionnaireRepo.Create(newQuestionnaire);
            return RedirectToAction(nameof(Index));
        }


        public ActionResult AddQuestionToQuestionnaire(string questionContent, int questionnaireId)
        {
            
            if (questionContent == string.Empty || questionContent is null) return RedirectToAction("ManageQuestions", new { id = questionnaireId });

            var newQuestion = new Question
            {
                Contents = questionContent,
                QuestionnaireId = questionnaireId
            };

            int id = questionnaireId;

            _questionRepo.Create(newQuestion).Wait();
            return RedirectToAction("ManageQuestions", new{id=id});
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
