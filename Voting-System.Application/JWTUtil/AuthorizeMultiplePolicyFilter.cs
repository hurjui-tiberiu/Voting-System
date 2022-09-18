using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Voting_System.Application.JWTUtil
{
    public class AuthorizeMultiplePolicyFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService authorization;
        public string policies { get; private set; }
        public bool isAll { get; set; }

        public AuthorizeMultiplePolicyFilter(string policies, bool IsAll, IAuthorizationService authorization)
        {
            this.policies = policies;
            this.authorization = authorization;
            isAll = IsAll;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var policys = policies.Split(";").ToList();
            if (isAll)
            {
                foreach (var policy in policys)
                {
                    var authorized = await authorization.AuthorizeAsync(context.HttpContext.User, policy);
                    if (!authorized.Succeeded)
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            }
            else
            {
                foreach (var policy in policys)
                {
                    var authorized = await authorization.AuthorizeAsync(context.HttpContext.User, policy);
                    if (authorized.Succeeded)
                    {
                        return;
                    }
                }
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
