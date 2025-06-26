using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using ToSic.Module.Cre8magicTests.Repository;
using System.Threading.Tasks;

namespace ToSic.Module.Cre8magicTests.Manager
{
    public class Cre8magicTestsManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly ICre8magicTestsRepository _Cre8magicTestsRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public Cre8magicTestsManager(ICre8magicTestsRepository Cre8magicTestsRepository, IDBContextDependencies DBContextDependencies)
        {
            _Cre8magicTestsRepository = Cre8magicTestsRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new Cre8magicTestsContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new Cre8magicTestsContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.Cre8magicTests> Cre8magicTestss = _Cre8magicTestsRepository.GetCre8magicTestss(module.ModuleId).ToList();
            if (Cre8magicTestss != null)
            {
                content = JsonSerializer.Serialize(Cre8magicTestss);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Cre8magicTests> Cre8magicTestss = null;
            if (!string.IsNullOrEmpty(content))
            {
                Cre8magicTestss = JsonSerializer.Deserialize<List<Models.Cre8magicTests>>(content);
            }
            if (Cre8magicTestss != null)
            {
                foreach(var Cre8magicTests in Cre8magicTestss)
                {
                    _Cre8magicTestsRepository.AddCre8magicTests(new Models.Cre8magicTests { ModuleId = module.ModuleId, Name = Cre8magicTests.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var Cre8magicTests in _Cre8magicTestsRepository.GetCre8magicTestss(pageModule.ModuleId))
           {
               if (Cre8magicTests.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "ToSicCre8magicTests",
                       EntityId = Cre8magicTests.Cre8magicTestsId.ToString(),
                       Title = Cre8magicTests.Name,
                       Body = Cre8magicTests.Name,
                       ContentModifiedBy = Cre8magicTests.ModifiedBy,
                       ContentModifiedOn = Cre8magicTests.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
