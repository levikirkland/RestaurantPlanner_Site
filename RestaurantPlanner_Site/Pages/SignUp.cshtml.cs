#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantPlanner_Site.Interfaces;
using RestaurantPlanner_Site.Models;
using RestaurantPlanner_Site.Services;

namespace RestaurantPlanner_Site.Pages

{
    public class SignUpModel : PageModel
    {
        private readonly ISignUpConfig _signUpConfig;

        public SignUpModel(ISignUpConfig signUpConfig)
        {
            _signUpConfig = signUpConfig;
        }

        [BindProperty]
        public int accountType { get; set; }

        public IActionResult OnGet(int id)
        {
            return Page();
        }

        [BindProperty]
        public AccountInfo AccountInfo { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await new AccountService(_signUpConfig).CreateAccountAsync(AccountInfo);
            if (result)
                return RedirectToPage("./Success");

            return RedirectToPage("./Error");
        }
    }
}
