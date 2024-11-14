using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using ToSic.Module.Cre8magicTests.Repository;
using ToSic.Module.Cre8magicTests.Services;

namespace ToSic.Module.Cre8magicTests.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICre8magicTestsService, ServerCre8magicTestsService>();
            services.AddDbContextFactory<Cre8magicTestsContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
