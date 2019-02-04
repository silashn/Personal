using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Models;
using System.Linq;

namespace Pages.Web.Pages.Admin.Membership
{
    public class UsersModel : PageModel
    {
        [BindProperty]
        public new Users User { get; set; }
        public IQueryable<Users> Users { get; set; }

        public string SystemMessage { get; set; }

        private IUserRepository userRepository;

        public UsersModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult OnGet(int? id)
        {
            Users = userRepository.GetUsers();
            if(id.HasValue)
            {
                User = userRepository.GetUser(id.Value);

                if(User == null)
                {
                    return Redirect("/Admin/Membership/Users");
                }
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if((User.Password == "" || User.Password == null) && id.HasValue)
            {
                User.Password = userRepository.GetUser(id.Value).Password;
            }

            if(ModelState.IsValid || (!ModelState.IsValid && ModelState.GetValidationState("User.Password") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid && ModelState.ErrorCount == 1))
            {
                if(!id.HasValue)
                {
                    SystemMessage = userRepository.Create(User);
                }
                else
                {
                    User.Id = id.Value;

                    SystemMessage = userRepository.Update(User);
                }

                TempData["SystemMessage"] = SystemMessage;
                return Redirect("/Admin/Membership/Users");
            }

            Users = userRepository.GetUsers();


            return Page();
        }
    }
}