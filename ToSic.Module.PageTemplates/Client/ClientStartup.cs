using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Oqtane.Services;
using ToSic.Module.PageTemplates.Services;
using ToSic.Module.PageTemplates.Shared;

namespace ToSic.Module.PageTemplates.Client
{
    internal class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddScoped<IPageTemplatesService, PageTemplatesService>();
        }
    }
}
