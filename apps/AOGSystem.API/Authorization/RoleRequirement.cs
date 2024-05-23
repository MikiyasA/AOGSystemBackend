using Microsoft.AspNetCore.Authorization;

namespace AOGSystem.API.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; }

        public RoleRequirement(params string[] roles)
        {
            Roles = roles;
        }
    }
}
