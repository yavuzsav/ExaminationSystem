using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.ErrorResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.Models.IdentityEntities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ExaminationSystem.Business.Concrete
{
    public class UserManager : BaseIdentityManager, IUserService
    {
        public UserManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base(userManager, signInManager, roleManager)
        {
        }

        public IDataResult<List<Claim>> GetClaims(UserDto user)
        {
            return new SuccessDataResult<List<Claim>>(UserManager.GetClaimsAsync(user.Adapt<AppUser>()).Result.ToList());
        }

        public IDataResult<List<string>> GetRoleNames(UserDto user)
        {
            var a = UserManager.FindByEmailAsync(user.Email).Result;

            var roleNames = UserManager.GetRolesAsync(a).Result;
            return new SuccessDataResult<List<string>>(roleNames.ToList());
        }

        public IResult Update(UserDto user, string userName)
        {
            AppUser appUser = UserManager.FindByNameAsync(user.UserName).Result;

            if (appUser != null)
            {
                appUser.Email = user.Email;
                appUser.UserName = user.UserName;

                IdentityResult result = UserManager.UpdateAsync(appUser).Result;

                if (result.Succeeded)
                    return new SuccessResult();
            }

            return new ErrorResult();
        }

        public IDataResult<Tuple<UserDto, IList<Claim>>> GetUserByRefreshToken(string refreshToken)
        {
            Claim claimRefreshToken = new Claim("refreshToken", refreshToken);

            var users = UserManager.GetUsersForClaimAsync(claimRefreshToken).Result;

            if (users.Any())
            {
                var user = users.FirstOrDefault();

                IList<Claim> userClaims = UserManager.GetClaimsAsync(user).Result;

                string refreshTokenEndDate = userClaims.FirstOrDefault(c => c.Type == "refreshTokenEndDate")?.Value;

                if (DateTime.Parse(refreshTokenEndDate) > DateTime.Now)
                {
                    var tuple = new Tuple<UserDto, IList<Claim>>(user.Adapt<UserDto>(), userClaims);
                    return new SuccessDataResult<Tuple<UserDto, IList<Claim>>>(tuple);
                }
            }

            return new ErrorDataResult<Tuple<UserDto, IList<Claim>>>(new Tuple<UserDto, IList<Claim>>(null, null));
        }

        public IResult RevokeRefreshToken(string refreshToken)
        {
            var result = GetUserByRefreshToken(refreshToken);

            if (result.Data.Item1 == null)
                return new ErrorResult();

            IdentityResult response = UserManager.RemoveClaimsAsync(result.Data.Item1.Adapt<AppUser>(), result.Data.Item2).Result;

            if (response.Succeeded)
                return new SuccessResult();

            return new ErrorResult();
        }

        public IDataResult<UserDto> GetByMail(string email)
        {
            var user = UserManager.FindByEmailAsync(email).Result;
            return new SuccessDataResult<UserDto>(user.Adapt<UserDto>());
        }

        public IDataResult<UserDto> GetByUserName(string userName)
        {
            var user = UserManager.FindByNameAsync(userName).Result;
            return new SuccessDataResult<UserDto>(user.Adapt<UserDto>());
        }

        public IDataResult<UserDto> GetByUserId(string id)
        {
            var user = UserManager.FindByIdAsync(id).Result;
            return new SuccessDataResult<UserDto>(user.Adapt<UserDto>());
        }

        public IDataResult<UserWithIdDto> GetUserByUserName(string userName)
        {
            var user = UserManager.FindByNameAsync(userName).Result;
            return new SuccessDataResult<UserWithIdDto>(user.Adapt<UserWithIdDto>());
        }
    }
}