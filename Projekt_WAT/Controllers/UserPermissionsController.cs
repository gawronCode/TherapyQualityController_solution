using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Models.ViewModels;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserPermissionsController : Controller
    {

        private readonly IQuestionnaireRepo _questionnaireRepo;
        private readonly IQuestionRepo _questionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAnswerRepo _answerRepo;
        private readonly IPatientQuestionnaireRepo _patientQuestionnaireRepo;
        private readonly UserManager<User> _userManager;

        public UserPermissionsController(IQuestionnaireRepo questionnaireRepo,
            IQuestionRepo questionRepo,
            IUserRepo userRepo,
            IAnswerRepo answerRepo,
            IPatientQuestionnaireRepo patientQuestionnaireRepo,
            UserManager<User> userManager)
        {
            _questionnaireRepo = questionnaireRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
            _answerRepo = answerRepo;
            _patientQuestionnaireRepo = patientQuestionnaireRepo;
            _userManager = userManager;
        }

        // GET: UserPermissionsController
        public ActionResult Index()
        {
            var patients = _userRepo.GetAll().Result;
            var model = new List<DoctorViewModel>();

            foreach (var patient in patients)
            {
                if (string.IsNullOrEmpty(patient.PWZ)) continue;
                if (_userManager.IsInRoleAsync(patient, "Doctor").Result)
                {
                    model.Add(new DoctorViewModel
                    {
                        EmailAddress = patient.Email,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        PESEL = patient.PESEL,
                        PWZ = patient.PWZ,
                        IsConfirmed = true
                    });
                }
                else
                {
                    model.Add(new DoctorViewModel
                    {
                        EmailAddress = patient.Email,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        PESEL = patient.PESEL,
                        PWZ = patient.PWZ,
                        IsConfirmed = false
                    });
                }
            }

            return View(model);
        }

        public ActionResult AddPermissions(string id)
        {
            var user = _userManager.FindByEmailAsync(id).Result;
            _userManager.RemoveFromRoleAsync(user, "Patient").Wait();
            _userManager.AddToRoleAsync(user, "Doctor").Wait();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult RemovePermissions(string id)
        {
            var user = _userManager.FindByEmailAsync(id).Result;
            _userManager.RemoveFromRoleAsync(user, "Doctor").Wait();
            _userManager.AddToRoleAsync(user, "Patient").Wait();

            return RedirectToAction(nameof(Index));
        }
    }
}
