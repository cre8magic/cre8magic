using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToSic.Module.Cre8magicTests.Services
{
    public interface ICre8magicTestsService 
    {
        Task<List<Models.Cre8magicTests>> GetCre8magicTestssAsync(int ModuleId);

        Task<Models.Cre8magicTests> GetCre8magicTestsAsync(int Cre8magicTestsId, int ModuleId);

        Task<Models.Cre8magicTests> AddCre8magicTestsAsync(Models.Cre8magicTests Cre8magicTests);

        Task<Models.Cre8magicTests> UpdateCre8magicTestsAsync(Models.Cre8magicTests Cre8magicTests);

        Task DeleteCre8magicTestsAsync(int Cre8magicTestsId, int ModuleId);
    }
}
