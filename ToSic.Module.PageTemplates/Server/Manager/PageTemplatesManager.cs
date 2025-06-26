using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Models;
using Oqtane.Modules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToSic.Module.PageTemplates.Manager
{
    public class PageTemplatesManager : MigratableModuleBase, IInstallable, IPortable/*, ISearchable*/
    {
        public PageTemplatesManager()
        {   }

        public bool Install(Tenant tenant, string version)
        {
            return true;
        }

        public bool Uninstall(Tenant tenant)
        {
            return true;
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            
        }

        //public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        //{
        //   var searchContentList = new List<SearchContent>();
        //   return Task.FromResult(searchContentList);
        //}
    }
}
