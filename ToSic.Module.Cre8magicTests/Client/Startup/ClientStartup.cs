using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using ToSic.Module.Cre8magicTests.Services;

namespace ToSic.Module.Cre8magicTests.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICre8magicTestsService, Cre8magicTestsService>();
        }
    }
}
