using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToSic.Module.Cre8magicTests.Repository
{
    public interface ICre8magicTestsRepository
    {
        IEnumerable<Models.Cre8magicTests> GetCre8magicTestss(int ModuleId);
        Models.Cre8magicTests GetCre8magicTests(int Cre8magicTestsId);
        Models.Cre8magicTests GetCre8magicTests(int Cre8magicTestsId, bool tracking);
        Models.Cre8magicTests AddCre8magicTests(Models.Cre8magicTests Cre8magicTests);
        Models.Cre8magicTests UpdateCre8magicTests(Models.Cre8magicTests Cre8magicTests);
        void DeleteCre8magicTests(int Cre8magicTestsId);
    }
}
