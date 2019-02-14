using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pages.Web.Pages.Admin.Membership
{
    public class UsersModel : PageModel
    {
        [BindProperty]
        public new User User { get; set; }
        [BindProperty]
        public Theme Theme { get; set; }
        public IEnumerable<User> Users { get; set; }

        public string SystemMessage { get; set; }

        private IUserRepository userRepository;
        private IThemeRepository themeRepository;

        public UsersModel(IUserRepository userRepository, IThemeRepository themeRepository)
        {
            this.userRepository = userRepository;
            this.themeRepository = themeRepository;
        }

        public IActionResult OnGet(string action, int? id, string themeName)
        {
            Users = userRepository.GetUsers();

            if(id.HasValue)
            {
                User = userRepository.GetUser(id.Value);

                if(User == null)
                {
                    return Redirect("/Admin/Membership/Users");
                }

                if(action == "Delete")
                {
                    TempData["SystemMessage"] = userRepository.Delete(User);
                    return Redirect("/Admin/Membership/Users");
                }

                if(themeName != null && themeName != "")
                {
                    Theme = User.Themes.FirstOrDefault(t => t.Name.Equals(themeName));
                    if(Theme == null)
                    {
                        return Redirect("/Admin/Membership/Users/Edit/" + id);
                    }

                    if(action == "ThemeDelete")
                    {
                        TempData["SystemMessage"] = themeRepository.Delete(Theme);
                        return Redirect("/Admin/Membership/Users/Edit/" + id);
                    }
                }
            }

            return Page();
        }

        public IActionResult OnPostUser(string action, int? id)
        {
            foreach(string key in ModelState.Keys.Where(k => k.Contains("Theme")))
            {
                ModelState.Remove(key);
            }

            if((User.Password == "" || User.Password == null) && id.HasValue)
            {
                User.Password = userRepository.GetUser(id.Value).Password;
                ModelState.Remove("User.Password");
            }

            if(ModelState.IsValid)
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

        public IActionResult OnPostTheme(string action, int? id, string themeName)
        {
            User = userRepository.GetUser(id.Value);
            foreach(string key in ModelState.Keys.Where(k => k.Contains("User")))
            {
                ModelState.Remove(key);
            }

            if(ModelState.IsValid)
            {
                if(User != null)
                {
                    if(themeName == null || themeName == "")
                    {
                        Theme.Color = Theme.Color.ToUpper();
                        SystemMessage = themeRepository.Create(Theme);
                    }
                    else
                    {
                        Theme UpdateTheme = User.Themes.FirstOrDefault(t => t.Name.Equals(themeName));
                        if(UpdateTheme != null)
                        {
                            Theme.Id = UpdateTheme.Id;
                            Theme.Color = Theme.Color.ToUpper();
                            SystemMessage = themeRepository.Update(Theme);
                        }
                    }
                }
                if(SystemMessage.ToLower().Contains("error"))
                {
                    Users = userRepository.GetUsers();
                    return Page();
                }
                TempData["SystemMessage"] = SystemMessage;
                return Redirect("/Admin/Membership/Users/Edit/" + User.Id);
            }

            Users = userRepository.GetUsers();
            return Page();
        }
    }
}