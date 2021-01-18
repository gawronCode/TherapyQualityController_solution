using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TherapyQualityController.Models;
using TherapyQualityController.Models.DbModels;
using TherapyQualityController.Repositories.IRepos;

namespace TherapyQualityController.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IPatientQuestionnaireRepo _patientQuestionnaireRepo;
        private readonly IUserAnswerRepo _userAnswerRepo;
        private readonly IUserQuestionnaireAnswerRepo _userQuestionnaireAnswerRepo;

        public DeletePersonalDataModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IPatientQuestionnaireRepo patientQuestionnaireRepo,
            IUserAnswerRepo userAnswerRepo,
            IUserQuestionnaireAnswerRepo userQuestionnaireAnswerRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _patientQuestionnaireRepo = patientQuestionnaireRepo;
            _userAnswerRepo = userAnswerRepo;
            _userQuestionnaireAnswerRepo = userQuestionnaireAnswerRepo;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Aby usunąć konto musisz podać hasło")]
            [DataType(DataType.Password)]
            [Display(Name = "Proszę potwierdzić decyzję wpisaniem hasła")]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            var patientQuestionnaires =
                _patientQuestionnaireRepo.GetPatientQuestionnairesByEmail(User.FindFirstValue(ClaimTypes.Email)).Result;

            foreach (var patientQuestionnaire in patientQuestionnaires)
            {
                _patientQuestionnaireRepo.Delete(patientQuestionnaire).Wait();
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userAnswers = _userAnswerRepo.GetUserAnswersByUserEmail(userEmail).Result;
            var questionnaireAnswered = _userQuestionnaireAnswerRepo.GetByUserEmail(userEmail).Result;

            foreach (var userAnswer in userAnswers)
            {
                _userAnswerRepo.Delete(userAnswer).Wait();
            }

            foreach (var questionnaire in questionnaireAnswered)
            {
                _userQuestionnaireAnswerRepo.Delete(questionnaire).Wait();
            }

            var result = await _userManager.DeleteAsync(user);


            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
