using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Models;
using System;
using System.Linq;

namespace Pages.Web.Pages.Admin.Membership
{
    public class UsersModel : PageModel
    {
        [BindProperty]
        public new Users User { get; set; }
        public IQueryable<Users> Users { get; set; }

        public string Error { get; set; }

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

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                if(User.Id == 0)
                {
                    Error = userRepository.Create(User);
                }
                else
                {
                    Error = userRepository.Update(User);
                }
            }

            return Redirect("/Admin/Membership/Users");
        }
    }
}