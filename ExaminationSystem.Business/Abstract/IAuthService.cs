using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Dtos.User;

namespace ExaminationSystem.Business.Abstract
{
    public interface IAuthService
    {
        IResult SignUp(UserForRegisterDto userForRegisterDto);

        IResult SignIn(UserForLoginDto userForLoginDto);

        void SignOut();

        IResult ResetPassword(UserForPasswordResetDto userForPasswordResetDto, string resetLink);

        IResult ResetPasswordConfirm(UserForPasswordResetDto userForPasswordResetDto, string token, string userId);
    }
}