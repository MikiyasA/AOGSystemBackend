using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace AOGSystem.API.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {

            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role && requirement.Roles.Contains(c.Value)))
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                _httpContextAccessor.HttpContext.Response.ContentType = "application/json";
                var message = new
                {
                    isSuccess = false,
                    message = "Forbidden: User does not have the required role."

                };
                var responseMessage = JsonConvert.SerializeObject(message);
                _httpContextAccessor.HttpContext.Response.WriteAsync(responseMessage);
                context.Fail(); 
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
