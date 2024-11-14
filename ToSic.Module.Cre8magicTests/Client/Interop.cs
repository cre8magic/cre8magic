using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ToSic.Module.Cre8magicTests
{
    public class Interop
    {
        private readonly IJSRuntime _jsRuntime;

        public Interop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
    }
}
