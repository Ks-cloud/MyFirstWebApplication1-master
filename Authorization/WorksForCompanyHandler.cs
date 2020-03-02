//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;

//namespace MyFirstWebApplication1.Authorization
//{
//    public class WorksForCompanyHandler : AuthorizationHandler<WorksForCompanyRequirement>
//    {
//        protected override Task HandlerRequirementAsync(AuthorizationHandlerContext context,
//            WorksForCompanyRequirement requirement)
//        {
//            /** check the Email of User */
//            /** if it doesn't exist -> return Empty */
//            var userEmailAddress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
//            if (userEmailAddress.EndsWith(requirement.DomainName))
//            {
//                context.Succeed(requirement);
//                return Task.CompletedTask;
//            }

//            context.Fail();
//            return Task.CompletedTask;
//        }
//    }
//}
