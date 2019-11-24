using ExaminationSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Business.Concrete
{
    public class BaseIdentityManager
    {
        protected UserManager<AppUser> UserManager;
        protected SignInManager<AppUser> SignInManager;
        protected RoleManager<AppRole> RoleManager;

        public BaseIdentityManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }
    }
}