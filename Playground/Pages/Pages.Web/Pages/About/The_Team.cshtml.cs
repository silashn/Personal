using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Models;
using System.Collections.Generic;

namespace Pages.Web.Pages.About
{
    public class The_TeamModel : PageModel
    {
        public List<Users> Users { get; set; }
        private IUserRepository userRepository;

        public The_TeamModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult OnGet()
        {
            Users = userRepository.GetUsers();
            return Page();
        }
    }
}