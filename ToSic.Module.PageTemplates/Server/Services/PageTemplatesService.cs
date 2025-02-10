using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Repository;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToSic.Module.PageTemplates.Shared;

namespace ToSic.Module.PageTemplates.Services
{
    public class ServerPageTemplatesService(
        ITenantManager tenantManager,
        ILogManager logger,
        ISiteRepository siteRepository,
        ISyncManager syncManager)
        : IPageTemplatesService
    {
        private readonly ILogManager _logger = logger;
        private readonly Alias _alias = tenantManager.GetAlias();

        public Task CreateTestAsync(string currentPage)
        {
            var site = siteRepository.GetSite(_alias.SiteId);

            // Level 0
            siteRepository.CreatePages(site, CreatePagesL0(currentPage), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            // Level 1
            siteRepository.CreatePages(site, CreatePagesL1(currentPage), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            // Level 2
            siteRepository.CreatePages(site, CreatePagesL2(currentPage), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            // Level 3
            siteRepository.CreatePages(site, CreatePagesL3(currentPage), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            // Level 4
            siteRepository.CreatePages(site, CreatePagesL4(currentPage), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            return Task.CompletedTask;
        }

        const string p1 = "PG1";
        const string p2 = "PG2";
        const string p3 = "PG3";
        const string p4 = "PG4";
        const string p5 = "PG5";

        private List<PageTemplate> CreatePagesL0(string currentPage)
        {
            List<string> parents = [currentPage];
            return
            [
                // Level 0
                Page(p1, parents),
                Page(p2, parents),
                Page(p3, parents),
                Page(p4, parents),
                Page(p5, parents),
            ];
        }

        private List<PageTemplate> CreatePagesL1(string currentPage)
        {
            return
            [
                // Level 1
                Page($"{p1}1", [currentPage, p1]),
                Page($"{p1}2", [currentPage, p1]),
                Page($"{p1}3", [currentPage, p1]),
                Page($"{p1}4", [currentPage, p1]),
                Page($"{p2}1", [currentPage, p2]),
                Page($"{p2}2", [currentPage, p2]),
                Page($"{p2}3", [currentPage, p2]),
                Page($"{p2}4", [currentPage, p2]),
                Page($"{p3}1", [currentPage, p3]),
                Page($"{p3}2", [currentPage, p3]),
                Page($"{p3}3", [currentPage, p3]),
                Page($"{p3}4", [currentPage, p3]),
                Page($"{p4}1", [currentPage, p4]),
                Page($"{p4}2", [currentPage, p4]),
                Page($"{p4}3", [currentPage, p4]),
                Page($"{p4}4", [currentPage, p4]),
                Page($"{p5}1", [currentPage, p5]),
                Page($"{p5}2", [currentPage, p5]),
                Page($"{p5}3", [currentPage, p5]),
                Page($"{p5}4", [currentPage, p5]),
                Page($"{p5}5", [currentPage, p5]),
            ];
        }

        private List<PageTemplate> CreatePagesL2(string currentPage)
        {
            return
            [
                // Level 2
                Page($"{p1}11", [currentPage, p1, $"{p1}1"]),
                Page($"{p1}12", [currentPage, p1, $"{p1}1"]),
                Page($"{p1}13", [currentPage, p1, $"{p1}1"]),
                Page($"{p1}14", [currentPage, p1, $"{p1}1"]),
                Page($"{p1}21", [currentPage, p1, $"{p1}2"]),
                Page($"{p1}22", [currentPage, p1, $"{p1}2"]),
                Page($"{p1}23", [currentPage, p1, $"{p1}2"]),
                Page($"{p1}24", [currentPage, p1, $"{p1}2"]),
                Page($"{p1}31", [currentPage, p1, $"{p1}3"]),
                Page($"{p1}32", [currentPage, p1, $"{p1}3"]),
                Page($"{p1}33", [currentPage, p1, $"{p1}3"]),
                Page($"{p1}34", [currentPage, p1, $"{p1}3"]),
                Page($"{p1}41", [currentPage, p1, $"{p1}4"]),
                Page($"{p1}42", [currentPage, p1, $"{p1}4"]),
                Page($"{p1}43", [currentPage, p1, $"{p1}4"]),
                Page($"{p1}44", [currentPage, p1, $"{p1}4"]),
                Page($"{p2}11", [currentPage, p2, $"{p2}1"]),
                Page($"{p2}12", [currentPage, p2, $"{p2}1"]),
                Page($"{p2}13", [currentPage, p2, $"{p2}1"]),
                Page($"{p2}14", [currentPage, p2, $"{p2}1"]),
                Page($"{p2}21", [currentPage, p2, $"{p2}2"]),
                Page($"{p2}22", [currentPage, p2, $"{p2}2"]),
                Page($"{p2}23", [currentPage, p2, $"{p2}2"]),
                Page($"{p2}24", [currentPage, p2, $"{p2}2"]),
                Page($"{p2}31", [currentPage, p2, $"{p2}3"]),
                Page($"{p2}32", [currentPage, p2, $"{p2}3"]),
                Page($"{p2}33", [currentPage, p2, $"{p2}3"]),
                Page($"{p2}34", [currentPage, p2, $"{p2}3"]),
                Page($"{p2}35", [currentPage, p2, $"{p2}3"]),
                Page($"{p2}41", [currentPage, p2, $"{p2}4"]),
                Page($"{p2}42", [currentPage, p2, $"{p2}4"]),
                Page($"{p2}43", [currentPage, p2, $"{p2}4"]),
                Page($"{p3}11", [currentPage, p3, $"{p3}1"]),
                Page($"{p3}12", [currentPage, p3, $"{p3}1"]),
                Page($"{p3}13", [currentPage, p3, $"{p3}1"]),
                Page($"{p3}14", [currentPage, p3, $"{p3}1"]),
                Page($"{p3}21", [currentPage, p3, $"{p3}2"]),
                Page($"{p3}22", [currentPage, p3, $"{p3}2"]),
                Page($"{p3}23", [currentPage, p3, $"{p3}2"]),
                Page($"{p3}24", [currentPage, p3, $"{p3}2"]),
                Page($"{p3}25", [currentPage, p3, $"{p3}2"]),
                Page($"{p3}31", [currentPage, p3, $"{p3}3"]),
                Page($"{p3}32", [currentPage, p3, $"{p3}3"]),
                Page($"{p3}33", [currentPage, p3, $"{p3}3"]),
                Page($"{p3}34", [currentPage, p3, $"{p3}3"]),
                Page($"{p3}41", [currentPage, p3, $"{p3}4"]),
                Page($"{p3}42", [currentPage, p3, $"{p3}4"]),
                Page($"{p3}43", [currentPage, p3, $"{p3}4"]),
                Page($"{p3}44", [currentPage, p3, $"{p3}4"]),
                Page($"{p4}11", [currentPage, p4, $"{p4}1"]),
                Page($"{p4}12", [currentPage, p4, $"{p4}1"]),
                Page($"{p4}13", [currentPage, p4, $"{p4}1"]),
                Page($"{p4}21", [currentPage, p4, $"{p4}2"]),
                Page($"{p4}31", [currentPage, p4, $"{p4}3"]),
                Page($"{p4}32", [currentPage, p4, $"{p4}3"]),
                Page($"{p4}41", [currentPage, p4, $"{p4}4"]),
                Page($"{p4}42", [currentPage, p4, $"{p4}4"]),
                Page($"{p4}43", [currentPage, p4, $"{p4}4"]),
            ];
        }

        private List<PageTemplate> CreatePagesL3(string currentPage)
        {
            return
            [
                // Level 3
                Page($"{p1}111", [currentPage, p1, $"{p1}1", $"{p1}11"]),
                Page($"{p1}112", [currentPage, p1, $"{p1}1", $"{p1}11"]),
                Page($"{p1}113", [currentPage, p1, $"{p1}1", $"{p1}11"]),
                Page($"{p1}114", [currentPage, p1, $"{p1}1", $"{p1}11"]),
                Page($"{p1}121", [currentPage, p1, $"{p1}1", $"{p1}12"]),
                Page($"{p1}122", [currentPage, p1, $"{p1}1", $"{p1}12"]),
                Page($"{p1}131", [currentPage, p1, $"{p1}1", $"{p1}13"]),
                Page($"{p1}132", [currentPage, p1, $"{p1}1", $"{p1}13"]),
                Page($"{p1}133", [currentPage, p1, $"{p1}1", $"{p1}13"]),
                Page($"{p1}141", [currentPage, p1, $"{p1}1", $"{p1}14"]),
                Page($"{p1}142", [currentPage, p1, $"{p1}1", $"{p1}14"]),
                Page($"{p1}143", [currentPage, p1, $"{p1}1", $"{p1}14"]),
                Page($"{p1}144", [currentPage, p1, $"{p1}1", $"{p1}14"]),
                Page($"{p1}211", [currentPage, p1, $"{p1}2", $"{p1}21"]),
                Page($"{p1}212", [currentPage, p1, $"{p1}2", $"{p1}21"]),
                Page($"{p1}213", [currentPage, p1, $"{p1}2", $"{p1}21"]),
                Page($"{p1}221", [currentPage, p1, $"{p1}2", $"{p1}22"]),
                Page($"{p1}222", [currentPage, p1, $"{p1}2", $"{p1}22"]),
                Page($"{p1}231", [currentPage, p1, $"{p1}2", $"{p1}23"]),
                Page($"{p1}232", [currentPage, p1, $"{p1}2", $"{p1}23"]),
                Page($"{p1}233", [currentPage, p1, $"{p1}2", $"{p1}23"]),
                Page($"{p1}241", [currentPage, p1, $"{p1}2", $"{p1}24"]),
                Page($"{p1}242", [currentPage, p1, $"{p1}2", $"{p1}24"]),
                Page($"{p1}243", [currentPage, p1, $"{p1}2", $"{p1}24"]),
                Page($"{p1}311", [currentPage, p1, $"{p1}3", $"{p1}31"]),
                Page($"{p1}312", [currentPage, p1, $"{p1}3", $"{p1}31"]),
                Page($"{p1}313", [currentPage, p1, $"{p1}3", $"{p1}31"]),
                Page($"{p1}314", [currentPage, p1, $"{p1}3", $"{p1}31"]),
                Page($"{p1}321", [currentPage, p1, $"{p1}3", $"{p1}32"]),
                Page($"{p1}322", [currentPage, p1, $"{p1}3", $"{p1}32"]),
                Page($"{p1}331", [currentPage, p1, $"{p1}3", $"{p1}33"]),
                Page($"{p1}332", [currentPage, p1, $"{p1}3", $"{p1}33"]),
                Page($"{p1}333", [currentPage, p1, $"{p1}3", $"{p1}33"]),
                Page($"{p1}341", [currentPage, p1, $"{p1}3", $"{p1}34"]),
                Page($"{p1}342", [currentPage, p1, $"{p1}3", $"{p1}34"]),
                Page($"{p1}343", [currentPage, p1, $"{p1}3", $"{p1}34"]),
                Page($"{p1}344", [currentPage, p1, $"{p1}3", $"{p1}34"]),
                Page($"{p1}411", [currentPage, p1, $"{p1}4", $"{p1}41"]),
                Page($"{p1}412", [currentPage, p1, $"{p1}4", $"{p1}41"]),
                Page($"{p1}421", [currentPage, p1, $"{p1}4", $"{p1}42"]),
                Page($"{p1}422", [currentPage, p1, $"{p1}4", $"{p1}42"]),
                Page($"{p1}431", [currentPage, p1, $"{p1}4", $"{p1}43"]),
                Page($"{p1}432", [currentPage, p1, $"{p1}4", $"{p1}43"]),
                Page($"{p1}433", [currentPage, p1, $"{p1}4", $"{p1}43"]),
                Page($"{p1}441", [currentPage, p1, $"{p1}4", $"{p1}44"]),
                Page($"{p1}442", [currentPage, p1, $"{p1}4", $"{p1}44"]),
                Page($"{p2}111", [currentPage, p2, $"{p2}1", $"{p2}11"]),
                Page($"{p2}112", [currentPage, p2, $"{p2}1", $"{p2}11"]),
                Page($"{p2}113", [currentPage, p2, $"{p2}1", $"{p2}11"]),
                Page($"{p2}114", [currentPage, p2, $"{p2}1", $"{p2}11"]),
                Page($"{p2}121", [currentPage, p2, $"{p2}1", $"{p2}12"]),
                Page($"{p2}122", [currentPage, p2, $"{p2}1", $"{p2}12"]),
                Page($"{p2}131", [currentPage, p2, $"{p2}1", $"{p2}13"]),
                Page($"{p2}132", [currentPage, p2, $"{p2}1", $"{p2}13"]),
                Page($"{p2}133", [currentPage, p2, $"{p2}1", $"{p2}13"]),
                Page($"{p2}141", [currentPage, p2, $"{p2}1", $"{p2}14"]),
                Page($"{p2}142", [currentPage, p2, $"{p2}1", $"{p2}14"]),
                Page($"{p2}143", [currentPage, p2, $"{p2}1", $"{p2}14"]),
                Page($"{p2}144", [currentPage, p2, $"{p2}1", $"{p2}14"]),
                Page($"{p2}211", [currentPage, p2, $"{p2}2", $"{p2}21"]),
                Page($"{p2}212", [currentPage, p2, $"{p2}2", $"{p2}21"]),
                Page($"{p2}213", [currentPage, p2, $"{p2}2", $"{p2}21"]),
                Page($"{p2}221", [currentPage, p2, $"{p2}2", $"{p2}22"]),
                Page($"{p2}222", [currentPage, p2, $"{p2}2", $"{p2}22"]),
                Page($"{p2}231", [currentPage, p2, $"{p2}2", $"{p2}23"]),
                Page($"{p2}232", [currentPage, p2, $"{p2}2", $"{p2}23"]),
                Page($"{p2}233", [currentPage, p2, $"{p2}2", $"{p2}23"]),
                Page($"{p2}241", [currentPage, p2, $"{p2}2", $"{p2}24"]),
                Page($"{p2}242", [currentPage, p2, $"{p2}2", $"{p2}24"]),
                Page($"{p2}311", [currentPage, p2, $"{p2}3", $"{p2}31"]),
                Page($"{p2}312", [currentPage, p2, $"{p2}3", $"{p2}31"]),
                Page($"{p2}313", [currentPage, p2, $"{p2}3", $"{p2}31"]),
                Page($"{p2}314", [currentPage, p2, $"{p2}3", $"{p2}31"]),
                Page($"{p2}321", [currentPage, p2, $"{p2}3", $"{p2}32"]),
                Page($"{p2}322", [currentPage, p2, $"{p2}3", $"{p2}32"]),
                Page($"{p2}331", [currentPage, p2, $"{p2}3", $"{p2}33"]),
                Page($"{p2}332", [currentPage, p2, $"{p2}3", $"{p2}33"]),
                Page($"{p2}333", [currentPage, p2, $"{p2}3", $"{p2}33"]),
                Page($"{p2}341", [currentPage, p2, $"{p2}3", $"{p2}34"]),
                Page($"{p2}342", [currentPage, p2, $"{p2}3", $"{p2}34"]),
                Page($"{p2}343", [currentPage, p2, $"{p2}3", $"{p2}34"]),
                Page($"{p3}111", [currentPage, p3, $"{p3}1", $"{p3}11"]),
                Page($"{p3}112", [currentPage, p3, $"{p3}1", $"{p3}11"]),
                Page($"{p3}113", [currentPage, p3, $"{p3}1", $"{p3}11"]),
                Page($"{p3}114", [currentPage, p3, $"{p3}1", $"{p3}11"]),
                Page($"{p3}121", [currentPage, p3, $"{p3}1", $"{p3}12"]),
                Page($"{p3}122", [currentPage, p3, $"{p3}1", $"{p3}12"]),
                Page($"{p3}131", [currentPage, p3, $"{p3}1", $"{p3}13"]),
                Page($"{p3}132", [currentPage, p3, $"{p3}1", $"{p3}13"]),
                Page($"{p3}133", [currentPage, p3, $"{p3}1", $"{p3}13"]),
                Page($"{p3}141", [currentPage, p3, $"{p3}1", $"{p3}14"]),
                Page($"{p3}142", [currentPage, p3, $"{p3}1", $"{p3}14"]),
                Page($"{p3}143", [currentPage, p3, $"{p3}1", $"{p3}14"]),
                Page($"{p3}144", [currentPage, p3, $"{p3}1", $"{p3}14"]),
                Page($"{p3}211", [currentPage, p3, $"{p3}2", $"{p3}21"]),
                Page($"{p3}212", [currentPage, p3, $"{p3}2", $"{p3}21"]),
                Page($"{p3}213", [currentPage, p3, $"{p3}2", $"{p3}21"]),
                Page($"{p3}221", [currentPage, p3, $"{p3}2", $"{p3}22"]),
                Page($"{p3}222", [currentPage, p3, $"{p3}2", $"{p3}22"]),
                Page($"{p3}231", [currentPage, p3, $"{p3}2", $"{p3}23"]),
                Page($"{p3}232", [currentPage, p3, $"{p3}2", $"{p3}23"]),
                Page($"{p3}233", [currentPage, p3, $"{p3}2", $"{p3}23"]),
                Page($"{p3}241", [currentPage, p3, $"{p3}2", $"{p3}24"]),
                Page($"{p3}242", [currentPage, p3, $"{p3}2", $"{p3}24"]),
                Page($"{p3}243", [currentPage, p3, $"{p3}2", $"{p3}24"]),
                Page($"{p3}311", [currentPage, p3, $"{p3}3", $"{p3}31"]),
                Page($"{p3}312", [currentPage, p3, $"{p3}3", $"{p3}31"]),
                Page($"{p3}313", [currentPage, p3, $"{p3}3", $"{p3}31"]),
                Page($"{p3}314", [currentPage, p3, $"{p3}3", $"{p3}31"]),
                Page($"{p3}321", [currentPage, p3, $"{p3}3", $"{p3}32"]),
                Page($"{p3}322", [currentPage, p3, $"{p3}3", $"{p3}32"]),
                Page($"{p3}331", [currentPage, p3, $"{p3}3", $"{p3}33"]),
                Page($"{p3}332", [currentPage, p3, $"{p3}3", $"{p3}33"]),
                Page($"{p3}333", [currentPage, p3, $"{p3}3", $"{p3}33"]),
                Page($"{p3}341", [currentPage, p3, $"{p3}3", $"{p3}34"]),
                Page($"{p3}342", [currentPage, p3, $"{p3}3", $"{p3}34"]),
                Page($"{p3}343", [currentPage, p3, $"{p3}3", $"{p3}34"]),
                Page($"{p3}344", [currentPage, p3, $"{p3}3", $"{p3}34"]),
                Page($"{p3}411", [currentPage, p3, $"{p3}4", $"{p3}41"]),
                Page($"{p3}412", [currentPage, p3, $"{p3}4", $"{p3}41"]),
                Page($"{p3}421", [currentPage, p3, $"{p3}4", $"{p3}42"]),
                Page($"{p3}422", [currentPage, p3, $"{p3}4", $"{p3}42"]),
                Page($"{p3}431", [currentPage, p3, $"{p3}4", $"{p3}43"]),
                Page($"{p3}432", [currentPage, p3, $"{p3}4", $"{p3}43"]),
                Page($"{p3}433", [currentPage, p3, $"{p3}4", $"{p3}43"]),
                Page($"{p3}441", [currentPage, p3, $"{p3}4", $"{p3}44"]),
                Page($"{p3}442", [currentPage, p3, $"{p3}4", $"{p3}44"]),
            ];
        }

        private List<PageTemplate> CreatePagesL4(string currentPage)
        {
            return
            [
                // Level 4
                Page($"{p2}1111", [currentPage, p2, $"{p2}1", $"{p2}11", $"{p2}111"]),
                Page($"{p2}1112", [currentPage, p2, $"{p2}1", $"{p2}11", $"{p2}111"]),
                Page($"{p2}1113", [currentPage, p2, $"{p2}1", $"{p2}11", $"{p2}111"]),
                Page($"{p2}1114", [currentPage, p2, $"{p2}1", $"{p2}11", $"{p2}111"]),
                Page($"{p2}1211", [currentPage, p2, $"{p2}1", $"{p2}12", $"{p2}121"]),
                Page($"{p2}1212", [currentPage, p2, $"{p2}1", $"{p2}12", $"{p2}121"]),
                Page($"{p2}1213", [currentPage, p2, $"{p2}1", $"{p2}12", $"{p2}121"]),
                Page($"{p2}1221", [currentPage, p2, $"{p2}1", $"{p2}12", $"{p2}122"]),
                Page($"{p2}1222", [currentPage, p2, $"{p2}1", $"{p2}12", $"{p2}122"]),
                Page($"{p2}1223", [currentPage, p2, $"{p2}1", $"{p2}12", $"{p2}122"]),
                Page($"{p2}1311", [currentPage, p2, $"{p2}1", $"{p2}13", $"{p2}131"]),
                Page($"{p2}1312", [currentPage, p2, $"{p2}1", $"{p2}13", $"{p2}131"]),
                Page($"{p2}1331", [currentPage, p2, $"{p2}1", $"{p2}13", $"{p2}133"]),
                Page($"{p2}1332", [currentPage, p2, $"{p2}1", $"{p2}13", $"{p2}133"]),
                Page($"{p2}1411", [currentPage, p2, $"{p2}1", $"{p2}14", $"{p2}141"]),
                Page($"{p2}1412", [currentPage, p2, $"{p2}1", $"{p2}14", $"{p2}141"]),
                Page($"{p2}1441", [currentPage, p2, $"{p2}1", $"{p2}14", $"{p2}144"]),
                Page($"{p2}1442", [currentPage, p2, $"{p2}1", $"{p2}14", $"{p2}144"]),
                Page($"{p2}1443", [currentPage, p2, $"{p2}1", $"{p2}14", $"{p2}144"]),
                Page($"{p2}2131", [currentPage, p2, $"{p2}2", $"{p2}21", $"{p2}213"]),
                Page($"{p2}2132", [currentPage, p2, $"{p2}2", $"{p2}21", $"{p2}213"]),
                Page($"{p2}2133", [currentPage, p2, $"{p2}2", $"{p2}21", $"{p2}213"]),
                Page($"{p2}2311", [currentPage, p2, $"{p2}2", $"{p2}23", $"{p2}231"]),
                Page($"{p2}2312", [currentPage, p2, $"{p2}2", $"{p2}23", $"{p2}231"]),
                Page($"{p2}2313", [currentPage, p2, $"{p2}2", $"{p2}23", $"{p2}231"]),
                Page($"{p2}2314", [currentPage, p2, $"{p2}2", $"{p2}23", $"{p2}231"]),
            ];
        }

        private static PageTemplate Page(string name, List<string> parents)
        {
            parents.RemoveAll(string.IsNullOrEmpty);
            return new PageTemplate
            {
                Name = name,
                Parent = Parent(parents),
                Path = Path(name, parents),
                PermissionList = EveryonePermissionList,
                PageTemplateModules =
                [
                    new()
                    {
                        ModuleDefinitionName = HtmlModule,
                        Title = name,
                        Pane = PaneNames.Default,
                        PermissionList = EveryonePermissionList,
                        Content = $"<p>{Path(name, parents)}</p>"
                    }
                ]
            };
        }

        private static string Parent(List<string> parents)
            => parents.Count == 0 ? string.Empty : string.Join("/", parents).ToLower();

        private static string Path(string name, List<string> parents)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;
            return parents.Count == 0
                ? name.ToLower()
                : string.Join("/", parents.Append(name)).ToLower();
        }

        private static List<Permission> EveryonePermissionList
            =>
        [
            new(PermissionNames.View, RoleNames.Everyone, true),
            new(PermissionNames.View, RoleNames.Admin, true),
            new(PermissionNames.Edit, RoleNames.Admin, true)
        ];

        private const string HtmlModule = "Oqtane.Modules.HtmlText, Oqtane.Client";
    }
}
