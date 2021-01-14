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
using TherapyQualityController.Repositories;

namespace TherapyQualityController.Controllers
{
    [Authorize]
    public class PatientQuestionnaireController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IPatientRepo _patientRepo;

        public PatientQuestionnaireController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IPatientRepo patientRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _patientRepo = patientRepo;

        }

        // GET: PatientQuestionnaireController
        public ActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userQuestionnaireId = _patientRepo.GetPatientQuestionnaireIdByEmail(userEmail).Result;
            
            var questionnaire = _questionnaireRepo.GetById(userQuestionnaireId).Result;
            var questions = _questionRepo.GetQuestionsByQuestionnaireId(userQuestionnaireId).Result;

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
            var answers = new List<Answer>();

            for (var i = 0; i < count; i++)
            {
                string data = form[$"opt{i.ToString()}"];
                var dataParsed = data.Split(',');
                answers.Add(new Answer
                {
                    AnswerDate = DateTime.Now,
                    QuestionId = Convert.ToInt32(dataParsed[0]),
                    Range = Convert.ToInt32(dataParsed[1])

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
