using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.ErrorResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.Models.IdentityEntities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Business.Concrete
{
    public class RoleManager : BaseIdentityManager, IRoleService
    {
        public RoleManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base(userManager, signInManager, roleManager)
        {
        }

        public IDataResult<List<UserDto>> GetUsersInRoleName(string roleName)
        {
            var appUsers = UserManager.GetUsersInRoleAsync(roleName).Result;
            List<UserDto> users = new List<UserDto>();

            if (appUsers != null)
                foreach (var appUser in appUsers)
                {
                    users.Add(appUser.Adapt<UserDto>());
                }

            return new SuccessDataResult<List<UserDto>>(users);
        }

        public IDataResult<string> GetRoleNamesByUser(UserDto user)
        {
            var a = UserManager.FindByEmailAsync(user.Email).Result;
            var roleNames = UserManager.GetRolesAsync(a).Result;
            return new SuccessDataResult<string>(roleNames.FirstOrDefault(), "");
        }

        public IDataResult<List<string>> GetAllRoleNames()
        {
            var nameList = RoleManager.Roles.Select(x => x.Name).ToList();
            return new SuccessDataResult<List<string>>(nameList);
        }

        public IResult RoleAssign(UserDto user, string roleName)
        {
            var appUser = UserManager.FindByNameAsync(user.UserName).Result;

            if (appUser == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var removeResult = UserManager.RemoveFromRolesAsync(appUser, UserManager.GetRolesAsync(appUser).Result).Result;
            var result = UserManager.AddToRoleAsync(appUser, roleName).Result;

            if (result.Succeeded)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}