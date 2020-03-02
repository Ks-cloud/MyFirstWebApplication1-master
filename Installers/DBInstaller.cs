﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApplication1.Data;
using Microsoft.Extensions.Configuration;
using MyFirstWebApplication1.Services;

namespace MyFirstWebApplication1.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                /** automatically add the RoleManager to the IdentityRole */
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            /** scoped means that the lifetime of this is the same as the lifetime of request - that's done for tracking */
            services.AddScoped<IPostService, PostService>();
        }
    }
}
