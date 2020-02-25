using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ExaminationSystem.Framework.Utilities.Security.User
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserName()
        {
            //var userName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userName = _httpContextAccessor.HttpContext.User?.Identities?.Select(x => x.Name)?.FirstOrDefault();

            return userName;
        }
    }
}
