using Microsoft.AspNetCore.Mvc.RazorPages;
using Pages.Data.Scaffolding.Models;

namespace Pages.Web.Pages.Admin.Membership
{
    public class UsersModel : PageModel
    {
        public Users user;

        public UsersModel(Users user)
        {
            this.user = user;
        }

        public void OnGet()
        {
        }
    }
}