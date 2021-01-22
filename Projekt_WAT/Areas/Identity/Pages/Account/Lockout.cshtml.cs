using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TherapyQualityController.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Administrator")]
    public class LockoutModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
