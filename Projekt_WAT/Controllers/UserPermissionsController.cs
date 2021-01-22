using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        private readonly IUserRepo _userRepo;
        private readonly UserManager<User> _userManager;

        public UserPermissionsController(IUserRepo userRepo,
            UserManager<User> userManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
        }


        public async Task<ActionResult> Index()
        {
            var patients = await _userRepo.GetAll();
            var model = new List<DoctorViewModel>();

            foreach (var patient in patients)
            {
                if (string.IsNullOrEmpty(patient.PWZ)) continue;
                if (await _userManager.IsInRoleAsync(patient, "Doctor"))
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

        public async Task<ActionResult> AddPermissions(string id)
        {
            var user = await _userManager.FindByEmailAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "Patient");
            await _userManager.AddToRoleAsync(user, "Doctor");

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> RemovePermissions(string id)
        {
            var user = await _userManager.FindByEmailAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "Doctor");
            await _userManager.AddToRoleAsync(user, "Patient");

            return RedirectToAction(nameof(Index));
        }
    }
}
