using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.Framework.Utilities.Helpers;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.ErrorResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;

namespace ExaminationSystem.Business.Concrete
{
    public class AuthManager : BaseIdentityManager, IAuthService
    {
        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base(userManager, signInManager, roleManager)
        {
        }

        public IResult SignUp(UserForRegisterDto userForRegisterDto)
        {
            //todo email doğrulama eklenebilir.
            if (!RoleManager.RoleExistsAsync("Student").Result)
            {
                RoleManager.CreateAsync(new AppRole
                {
                    Name = "Student",
                });
            }

            if (!RoleManager.RoleExistsAsync("Admin").Result)
            {
                RoleManager.CreateAsync(new AppRole
                {
                    Name = "Admin",
                });
            }

            AppUser user = new AppUser
            {
                UserName = userForRegisterDto.UserName,
                Email = userForRegisterDto.Email
            };
            IdentityResult result = UserManager.CreateAsync(user, userForRegisterDto.Password).Result;

            if (result.Succeeded)
            {
                var roleResult = UserManager.AddToRoleAsync(user, "Student").Result;
                if (roleResult.Succeeded)
                    return new SuccessResult();
                else
                    return new ErrorResult(Messages.RoleAssignError);
            }

            UserManager.DeleteAsync(user);
            return new ErrorResult(result.Errors.ToString());
        }

        public IResult SignIn(UserForLoginDto userForLoginDto)
        {
            var user = UserManager.FindByEmailAsync(userForLoginDto.Email).Result;

            if (user == null)
                return new ErrorResult(Messages.UserNotExists);//user yok

            if (UserManager.IsLockedOutAsync(user).Result)
                return new ErrorResult(Messages.UserLocked); //hesap kitli

            //todo email doğrulaması varsa ekle

            SignInManager.SignOutAsync();
            SignInResult result = SignInManager.PasswordSignInAsync(user, userForLoginDto.Password, userForLoginDto.RememberMe, false)
                .Result;

            if (result.Succeeded)
            {
                UserManager.ResetAccessFailedCountAsync(user);
                return new SuccessResult();
            }

            UserManager.AccessFailedAsync(user);
            int fail = UserManager.GetAccessFailedCountAsync(user).Result;

            if (fail == 5)
            {
                UserManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(20)));
                return new ErrorResult(Messages.UserLocked);//hesap kilitlendi
            }

            return new ErrorResult();
        }

        public void SignOut()
        {
            SignInManager.SignOutAsync();
        }

        public IResult ResetPassword(UserForPasswordResetDto userForPasswordResetDto, string resetLink)
        {
            var user = UserManager.FindByEmailAsync(userForPasswordResetDto.Email).Result;

            if (user == null)
                return new ErrorResult(Messages.UserNotFound);

            string passwordResetToken = UserManager.GeneratePasswordResetTokenAsync(user).Result;

            //if(//todo reset mail)
            return new SuccessResult();

            return new ErrorResult();
        }

        public IResult ResetPasswordConfirm(UserForPasswordResetDto userForPasswordResetDto, string token, string userId)
        {
            var user = UserManager.FindByIdAsync(userId).Result;

            if (user == null)
                return new ErrorResult(Messages.GeneralErrorMessage);

            IdentityResult result = UserManager.ResetPasswordAsync(user, token, userForPasswordResetDto.NewPassword).Result;

            if (result.Succeeded)
            {
                UserManager.UpdateSecurityStampAsync(user);
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}