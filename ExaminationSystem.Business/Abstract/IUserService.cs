using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ExaminationSystem.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<Claim>> GetClaims(UserDto user);

        //IResult Add(AppUser user);

        IResult Update(UserDto user, string userName);

        IDataResult<Tuple<UserDto, IList<Claim>>> GetUserByRefreshToken(string refreshToken);

        IResult RevokeRefreshToken(string refreshToken);

        IDataResult<UserDto> GetByMail(string email);

        IDataResult<UserDto> GetByUserName(string userName);

        IDataResult<UserDto> GetByUserId(string id);
    }
}