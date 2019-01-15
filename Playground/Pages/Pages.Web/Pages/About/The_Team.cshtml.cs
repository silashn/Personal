using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pages.Data.Models.Membership;
using Pages.Data.Repositories.Interfaces;

namespace Pages.Web.Pages.About
{
    public class The_TeamModel : PageModel
    {
        public List<User> Users { get; set; }
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