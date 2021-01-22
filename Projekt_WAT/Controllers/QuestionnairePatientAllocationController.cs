using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Models.ViewModels;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Controllers
{
    [Authorize(Roles = "Administrator,Doctor")]
    public class QuestionnairePatientAllocationController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IPatientQuestionnaireRepo _patientQuestionnaireRepo;
        private readonly UserManager<User> _userManager;
        private readonly IUserAnswerRepo _userAnswerRepo;
        private readonly IUserQuestionnaireAnswerRepo _userQuestionnaireAnswerRepo;

        public QuestionnairePatientAllocationController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IPatientQuestionnaireRepo patientQuestionnaireRepo,
            UserManager<User> userManager,
            IUserAnswerRepo userAnswerRepo,
            IUserQuestionnaireAnswerRepo userQuestionnaireAnswerRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _patientQuestionnaireRepo = patientQuestionnaireRepo;
            _userManager = userManager;
            _userAnswerRepo = userAnswerRepo;
            _userQuestionnaireAnswerRepo = userQuestionnaireAnswerRepo;
        }


        public async Task<ActionResult> Index()
        {
            var patients = await _userRepo.GetAll();
            var model = (from patient in patients 
                where  _userManager.IsInRoleAsync(patient, "Patient").Result 
                select new PatientViewModel {EmailAddress = patient.Email, 
                    FirstName = patient.FirstName, 
                    LastName = patient.LastName, 
                    PESEL = patient.PESEL}).ToList();

            return View(model);
        }

        public async Task<ActionResult> ManagePatientQuestionnaires(string id)
        {

            var patientQuestionnaires = await _patientQuestionnaireRepo.GetPatientQuestionnairesByEmail(id);
            var patientQuestionnaireViewModels = patientQuestionnaires.Select(patientQuestionnaire => 
                new PatientQuestionnaireViewModel {PatientEmail = patientQuestionnaire.PatientEmail, 
                    QuestionnaireId = patientQuestionnaire.QuestionnaireId}).ToList();

            var questionnaires = await _questionnaireRepo.GetAll();
            var questionnaireViewModels = questionnaires.Select(questionnaire => 
                new QuestionnaireViewModel { Fields = null, 
                    Id = questionnaire.Id, 
                    Name = questionnaire.Name }).ToList();

            foreach (var item in patientQuestionnaireViewModels)
            {
                item.QuestionnaireName = _questionnaireRepo.GetById(item.QuestionnaireId).Result.Name;
                if (questionnaireViewModels.Any(x => 
                    x.Id == item.QuestionnaireId))
                    questionnaireViewModels.Remove(
                        questionnaireViewModels.FirstOrDefault(q => 
                            q.Id == item.QuestionnaireId));
            }

            var model = new PatientQuestionnaireManagerViewModel
            {
                patientEmail = id,
                PatientQuestionnaires = patientQuestionnaireViewModels,
                Questionnaires = questionnaireViewModels
            };

            return View(model);
        }

        public async Task<ActionResult> AssignQuestionnaireToPatient(int id, string email)
        {
            await _patientQuestionnaireRepo.Create(new UserQuestionnaire
            {
                PatientEmail = email,
                QuestionnaireId = id
            });

            return RedirectToAction("ManagePatientQuestionnaires", new { id = email });
        }

        public async Task<ActionResult> RemoveQuestionnaireFromPatient(int id, string email)
        {
            var patientQuestionnaire = await _patientQuestionnaireRepo.GetByIdAndUserEmail(id, email);
            await _patientQuestionnaireRepo.Delete(patientQuestionnaire);

            var userAnswers = await _userAnswerRepo.GetUserAnswersByUserEmail(email);
            var answersToSelectedQuestionnaire = userAnswers.Where(answer =>
                _questionRepo.GetById(answer.QuestionId).Result.QuestionnaireId == id).ToList();
            var questionnaireAnswered = _userQuestionnaireAnswerRepo.GetByUserEmailAndQuestionnaireId(email, id).Result;

            foreach (var userAnswer in answersToSelectedQuestionnaire)
            {
                await _userAnswerRepo.Delete(userAnswer);
            }

            foreach (var questionnaire in questionnaireAnswered)
            {
                await _userQuestionnaireAnswerRepo.Delete(questionnaire);
            }
            
            return RedirectToAction("ManagePatientQuestionnaires", new { id = email });
        }

    }
}
