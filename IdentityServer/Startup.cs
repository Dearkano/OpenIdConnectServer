// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sakura.AspNetCore.Mvc;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data.MyDbContext>(options =>
            {
                options.UseMySql("Data Source=111.231.75.113,3306;Initial Catalog=zju;User ID=zju;Password=123456;");
            });
            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers())
                .AddResourceOwnerValidator<MyResourceOwnerValidatior>()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddProfileService<myIProfileService>()
                ;
            
          
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddMvc(options =>
            {
                options.EnableActionResultExceptionFilter(); // ActionResult 异常
            });
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpMethodOverride();
            app.UseCors("AllowSpecificOrigin");
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}