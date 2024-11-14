using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace ToSic.Module.Cre8magicTests.Services
{
    public class Cre8magicTestsService : ServiceBase, ICre8magicTestsService
    {
        public Cre8magicTestsService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Cre8magicTests");

        public async Task<List<Models.Cre8magicTests>> GetCre8magicTestssAsync(int ModuleId)
        {
            List<Models.Cre8magicTests> Cre8magicTestss = await GetJsonAsync<List<Models.Cre8magicTests>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.Cre8magicTests>().ToList());
            return Cre8magicTestss.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Cre8magicTests> GetCre8magicTestsAsync(int Cre8magicTestsId, int ModuleId)
        {
            return await GetJsonAsync<Models.Cre8magicTests>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Cre8magicTestsId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Cre8magicTests> AddCre8magicTestsAsync(Models.Cre8magicTests Cre8magicTests)
        {
            return await PostJsonAsync<Models.Cre8magicTests>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Cre8magicTests.ModuleId), Cre8magicTests);
        }

        public async Task<Models.Cre8magicTests> UpdateCre8magicTestsAsync(Models.Cre8magicTests Cre8magicTests)
        {
            return await PutJsonAsync<Models.Cre8magicTests>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Cre8magicTests.Cre8magicTestsId}", EntityNames.Module, Cre8magicTests.ModuleId), Cre8magicTests);
        }

        public async Task DeleteCre8magicTestsAsync(int Cre8magicTestsId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{Cre8magicTestsId}", EntityNames.Module, ModuleId));
        }
    }
}
