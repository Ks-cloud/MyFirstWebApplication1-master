using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyFirstWebApplication1.Extensions
{
    public static class GeneralExtensions
    {
        /** HttpContext - a bunch of HTTP-props about a current HTTP-request in a controller */
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }
            /**
             * return the userId from the token, because the claim will automatically rebound to this object called User 
             */
            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}
