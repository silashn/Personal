using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pages.Web.Pages.About
{
    public class The_TeamModel : PageModel
    {
        public IEnumerable<User> Users { get; set; }
        private IUserRepository userRepository;

        public The_TeamModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void OnGet(int? id)
        {
            Users = userRepository.GetUsers();
        }
    }
}