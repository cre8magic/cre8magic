using System.Threading.Tasks;

namespace ToSic.Module.PageTemplates.Shared
{
    public interface IPageTemplatesService 
    {
        Task CreateTestAsync(string currentPage);
    }
}
