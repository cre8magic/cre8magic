using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Oqtane.Infrastructure;
using System;
using ToSic.Module.PageTemplates.Services;
using ToSic.Module.PageTemplates.Shared;

namespace ToSic.Module.PageTemplates.Server
{
    public class Startup : IServerStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddTransient<ServerPageTemplatesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //throw new NotImplementedException();
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            //throw new NotImplementedException();
        }
    }
}
