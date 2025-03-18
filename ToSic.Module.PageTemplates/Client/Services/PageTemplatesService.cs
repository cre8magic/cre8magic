using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ToSic.Module.PageTemplates.Shared;

namespace ToSic.Module.PageTemplates.Services
{
    public class PageTemplatesService(IHttpClientFactory http, SiteState siteState)
        : ServiceBase(http, siteState), IPageTemplatesService
    {
        private string Apiurl => CreateApiUrl("PageTemplates");

        public Task CreateTestAsync(string currentPage)
        {
            return GetAsync($"{Apiurl}?currentPage={Uri.EscapeDataString(currentPage)}");
        }
    }
}
