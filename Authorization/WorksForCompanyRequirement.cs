using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyFirstWebApplication1.Authorization 
{
    public class WorksForCompanyRequirement : IAuthorizationRequirement
    {
        /** props */
        public string DomainName { get; }

        public WorksForCompanyRequirement(string domainName)
        {
            DomainName = domainName;
        }
    }
}
