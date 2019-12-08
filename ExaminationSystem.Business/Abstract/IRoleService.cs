using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Dtos.User;
using System.Collections.Generic;

namespace ExaminationSystem.Business.Abstract
{
    public interface IRoleService
    {
        IDataResult<List<UserDto>> GetUsersInRoleName(string roleName);

        IDataResult<string> GetRoleNamesByUser(UserDto user);

        IDataResult<List<string>> GetAllRoleNames();

        IResult RoleAssign(UserDto user, string roleName);
    }
}