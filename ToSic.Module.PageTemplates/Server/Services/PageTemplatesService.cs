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

        public Task CreateTestAsync()
        {
            var site = siteRepository.GetSite(_alias.SiteId);

            // Level 0
            siteRepository.CreatePages(site, CreatePagesL0(), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            // Level 1
            siteRepository.CreatePages(site, CreatePagesL1(), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            // Level 2
            siteRepository.CreatePages(site, CreatePagesL2(), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            // Level 3
            siteRepository.CreatePages(site, CreatePagesL3(), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            // Level 4
            siteRepository.CreatePages(site, CreatePagesL4(), null);
            syncManager.AddSyncEvent(_alias, EntityNames.Site, _alias.SiteId, SyncEventActions.Refresh);

            return Task.CompletedTask;
        }

        const string p1 = "PG1";
        const string p2 = "PG2";
        const string p3 = "PG3";
        const string p4 = "PG4";
        const string p5 = "PG5";

        private List<PageTemplate> CreatePagesL0()
        {
            return
            [
                // Level 0
                Page(p1, []),
                Page(p2, []),
                Page(p3, []),
                Page(p4, []),
                Page(p5, []),
            ];
        }

        private List<PageTemplate> CreatePagesL1()
        {
            return
            [
                // Level 1
                Page($"{p1}1", [p1]),
                Page($"{p1}2", [p1]),
                Page($"{p1}3", [p1]),
                Page($"{p1}4", [p1]),
                Page($"{p2}1", [p2]),
                Page($"{p2}2", [p2]),
                Page($"{p2}3", [p2]),
                Page($"{p2}4", [p2]),
                Page($"{p3}1", [p3]),
                Page($"{p3}2", [p3]),
                Page($"{p3}3", [p3]),
                Page($"{p3}4", [p3]),
                Page($"{p4}1", [p4]),
                Page($"{p4}2", [p4]),
                Page($"{p4}3", [p4]),
                Page($"{p4}4", [p4]),
                Page($"{p5}1", [p5]),
                Page($"{p5}2", [p5]),
                Page($"{p5}3", [p5]),
                Page($"{p5}4", [p5]),
                Page($"{p5}5", [p5]),
            ];
        }

        private List<PageTemplate> CreatePagesL2()
        {
            return
            [
                // Level 2
                Page($"{p1}11", [p1, $"{p1}1"]),
                Page($"{p1}12", [p1, $"{p1}1"]),
                Page($"{p1}13", [p1, $"{p1}1"]),
                Page($"{p1}14", [p1, $"{p1}1"]),
                Page($"{p1}21", [p1, $"{p1}2"]),
                Page($"{p1}22", [p1, $"{p1}2"]),
                Page($"{p1}23", [p1, $"{p1}2"]),
                Page($"{p1}24", [p1, $"{p1}2"]),
                Page($"{p1}31", [p1, $"{p1}3"]),
                Page($"{p1}32", [p1, $"{p1}3"]),
                Page($"{p1}33", [p1, $"{p1}3"]),
                Page($"{p1}34", [p1, $"{p1}3"]),
                Page($"{p1}41", [p1, $"{p1}4"]),
                Page($"{p1}42", [p1, $"{p1}4"]),
                Page($"{p1}43", [p1, $"{p1}4"]),
                Page($"{p1}44", [p1, $"{p1}4"]),
                Page($"{p2}11", [p2, $"{p2}1"]),
                Page($"{p2}12", [p2, $"{p2}1"]),
                Page($"{p2}13", [p2, $"{p2}1"]),
                Page($"{p2}14", [p2, $"{p2}1"]),
                Page($"{p2}21", [p2, $"{p2}2"]),
                Page($"{p2}22", [p2, $"{p2}2"]),
                Page($"{p2}23", [p2, $"{p2}2"]),
                Page($"{p2}24", [p2, $"{p2}2"]),
                Page($"{p2}31", [p2, $"{p2}3"]),
                Page($"{p2}32", [p2, $"{p2}3"]),
                Page($"{p2}33", [p2, $"{p2}3"]),
                Page($"{p2}34", [p2, $"{p2}3"]),
                Page($"{p2}35", [p2, $"{p2}3"]),
                Page($"{p2}41", [p2, $"{p2}4"]),
                Page($"{p2}42", [p2, $"{p2}4"]),
                Page($"{p2}43", [p2, $"{p2}4"]),
                Page($"{p3}11", [p3, $"{p3}1"]),
                Page($"{p3}12", [p3, $"{p3}1"]),
                Page($"{p3}13", [p3, $"{p3}1"]),
                Page($"{p3}14", [p3, $"{p3}1"]),
                Page($"{p3}21", [p3, $"{p3}2"]),
                Page($"{p3}22", [p3, $"{p3}2"]),
                Page($"{p3}23", [p3, $"{p3}2"]),
                Page($"{p3}24", [p3, $"{p3}2"]),
                Page($"{p3}25", [p3, $"{p3}2"]),
                Page($"{p3}31", [p3, $"{p3}3"]),
                Page($"{p3}32", [p3, $"{p3}3"]),
                Page($"{p3}33", [p3, $"{p3}3"]),
                Page($"{p3}34", [p3, $"{p3}3"]),
                Page($"{p3}41", [p3, $"{p3}4"]),
                Page($"{p3}42", [p3, $"{p3}4"]),
                Page($"{p3}43", [p3, $"{p3}4"]),
                Page($"{p3}44", [p3, $"{p3}4"]),
                Page($"{p4}11", [p4, $"{p4}1"]),
                Page($"{p4}12", [p4, $"{p4}1"]),
                Page($"{p4}13", [p4, $"{p4}1"]),
                Page($"{p4}21", [p4, $"{p4}2"]),
                Page($"{p4}31", [p4, $"{p4}3"]),
                Page($"{p4}32", [p4, $"{p4}3"]),
                Page($"{p4}41", [p4, $"{p4}4"]),
                Page($"{p4}42", [p4, $"{p4}4"]),
                Page($"{p4}43", [p4, $"{p4}4"]),
            ];
        }

        private List<PageTemplate> CreatePagesL3()
        {
            return
            [
                // Level 3
                Page($"{p1}111", [p1, $"{p1}1", $"{p1}11"]),
                Page($"{p1}112", [p1, $"{p1}1", $"{p1}11"]),
                Page($"{p1}113", [p1, $"{p1}1", $"{p1}11"]),
                Page($"{p1}114", [p1, $"{p1}1", $"{p1}11"]),
                Page($"{p1}121", [p1, $"{p1}1", $"{p1}12"]),
                Page($"{p1}122", [p1, $"{p1}1", $"{p1}12"]),
                Page($"{p1}131", [p1, $"{p1}1", $"{p1}13"]),
                Page($"{p1}132", [p1, $"{p1}1", $"{p1}13"]),
                Page($"{p1}133", [p1, $"{p1}1", $"{p1}13"]),
                Page($"{p1}141", [p1, $"{p1}1", $"{p1}14"]),
                Page($"{p1}142", [p1, $"{p1}1", $"{p1}14"]),
                Page($"{p1}143", [p1, $"{p1}1", $"{p1}14"]),
                Page($"{p1}144", [p1, $"{p1}1", $"{p1}14"]),
                Page($"{p1}211", [p1, $"{p1}2", $"{p1}21"]),
                Page($"{p1}212", [p1, $"{p1}2", $"{p1}21"]),
                Page($"{p1}213", [p1, $"{p1}2", $"{p1}21"]),
                Page($"{p1}221", [p1, $"{p1}2", $"{p1}22"]),
                Page($"{p1}222", [p1, $"{p1}2", $"{p1}22"]),
                Page($"{p1}231", [p1, $"{p1}2", $"{p1}23"]),
                Page($"{p1}232", [p1, $"{p1}2", $"{p1}23"]),
                Page($"{p1}233", [p1, $"{p1}2", $"{p1}23"]),
                Page($"{p1}241", [p1, $"{p1}2", $"{p1}24"]),
                Page($"{p1}242", [p1, $"{p1}2", $"{p1}24"]),
                Page($"{p1}243", [p1, $"{p1}2", $"{p1}24"]),
                Page($"{p1}311", [p1, $"{p1}3", $"{p1}31"]),
                Page($"{p1}312", [p1, $"{p1}3", $"{p1}31"]),
                Page($"{p1}313", [p1, $"{p1}3", $"{p1}31"]),
                Page($"{p1}314", [p1, $"{p1}3", $"{p1}31"]),
                Page($"{p1}321", [p1, $"{p1}3", $"{p1}32"]),
                Page($"{p1}322", [p1, $"{p1}3", $"{p1}32"]),
                Page($"{p1}331", [p1, $"{p1}3", $"{p1}33"]),
                Page($"{p1}332", [p1, $"{p1}3", $"{p1}33"]),
                Page($"{p1}333", [p1, $"{p1}3", $"{p1}33"]),
                Page($"{p1}341", [p1, $"{p1}3", $"{p1}34"]),
                Page($"{p1}342", [p1, $"{p1}3", $"{p1}34"]),
                Page($"{p1}343", [p1, $"{p1}3", $"{p1}34"]),
                Page($"{p1}344", [p1, $"{p1}3", $"{p1}34"]),
                Page($"{p1}411", [p1, $"{p1}4", $"{p1}41"]),
                Page($"{p1}412", [p1, $"{p1}4", $"{p1}41"]),
                Page($"{p1}421", [p1, $"{p1}4", $"{p1}42"]),
                Page($"{p1}422", [p1, $"{p1}4", $"{p1}42"]),
                Page($"{p1}431", [p1, $"{p1}4", $"{p1}43"]),
                Page($"{p1}432", [p1, $"{p1}4", $"{p1}43"]),
                Page($"{p1}433", [p1, $"{p1}4", $"{p1}43"]),
                Page($"{p1}441", [p1, $"{p1}4", $"{p1}44"]),
                Page($"{p1}442", [p1, $"{p1}4", $"{p1}44"]),
                Page($"{p2}111", [p2, $"{p2}1", $"{p2}11"]),
                Page($"{p2}112", [p2, $"{p2}1", $"{p2}11"]),
                Page($"{p2}113", [p2, $"{p2}1", $"{p2}11"]),
                Page($"{p2}114", [p2, $"{p2}1", $"{p2}11"]),
                Page($"{p2}121", [p2, $"{p2}1", $"{p2}12"]),
                Page($"{p2}122", [p2, $"{p2}1", $"{p2}12"]),
                Page($"{p2}131", [p2, $"{p2}1", $"{p2}13"]),
                Page($"{p2}132", [p2, $"{p2}1", $"{p2}13"]),
                Page($"{p2}133", [p2, $"{p2}1", $"{p2}13"]),
                Page($"{p2}141", [p2, $"{p2}1", $"{p2}14"]),
                Page($"{p2}142", [p2, $"{p2}1", $"{p2}14"]),
                Page($"{p2}143", [p2, $"{p2}1", $"{p2}14"]),
                Page($"{p2}144", [p2, $"{p2}1", $"{p2}14"]),
                Page($"{p2}211", [p2, $"{p2}2", $"{p2}21"]),
                Page($"{p2}212", [p2, $"{p2}2", $"{p2}21"]),
                Page($"{p2}213", [p2, $"{p2}2", $"{p2}21"]),
                Page($"{p2}221", [p2, $"{p2}2", $"{p2}22"]),
                Page($"{p2}222", [p2, $"{p2}2", $"{p2}22"]),
                Page($"{p2}231", [p2, $"{p2}2", $"{p2}23"]),
                Page($"{p2}232", [p2, $"{p2}2", $"{p2}23"]),
                Page($"{p2}233", [p2, $"{p2}2", $"{p2}23"]),
                Page($"{p2}241", [p2, $"{p2}2", $"{p2}24"]),
                Page($"{p2}242", [p2, $"{p2}2", $"{p2}24"]),
                Page($"{p2}311", [p2, $"{p2}3", $"{p2}31"]),
                Page($"{p2}312", [p2, $"{p2}3", $"{p2}31"]),
                Page($"{p2}313", [p2, $"{p2}3", $"{p2}31"]),
                Page($"{p2}314", [p2, $"{p2}3", $"{p2}31"]),
                Page($"{p2}321", [p2, $"{p2}3", $"{p2}32"]),
                Page($"{p2}322", [p2, $"{p2}3", $"{p2}32"]),
                Page($"{p2}331", [p2, $"{p2}3", $"{p2}33"]),
                Page($"{p2}332", [p2, $"{p2}3", $"{p2}33"]),
                Page($"{p2}333", [p2, $"{p2}3", $"{p2}33"]),
                Page($"{p2}341", [p2, $"{p2}3", $"{p2}34"]),
                Page($"{p2}342", [p2, $"{p2}3", $"{p2}34"]),
                Page($"{p2}343", [p2, $"{p2}3", $"{p2}34"]),
                Page($"{p3}111", [p3, $"{p3}1", $"{p3}11"]),
                Page($"{p3}112", [p3, $"{p3}1", $"{p3}11"]),
                Page($"{p3}113", [p3, $"{p3}1", $"{p3}11"]),
                Page($"{p3}114", [p3, $"{p3}1", $"{p3}11"]),
                Page($"{p3}121", [p3, $"{p3}1", $"{p3}12"]),
                Page($"{p3}122", [p3, $"{p3}1", $"{p3}12"]),
                Page($"{p3}131", [p3, $"{p3}1", $"{p3}13"]),
                Page($"{p3}132", [p3, $"{p3}1", $"{p3}13"]),
                Page($"{p3}133", [p3, $"{p3}1", $"{p3}13"]),
                Page($"{p3}141", [p3, $"{p3}1", $"{p3}14"]),
                Page($"{p3}142", [p3, $"{p3}1", $"{p3}14"]),
                Page($"{p3}143", [p3, $"{p3}1", $"{p3}14"]),
                Page($"{p3}144", [p3, $"{p3}1", $"{p3}14"]),
                Page($"{p3}211", [p3, $"{p3}2", $"{p3}21"]),
                Page($"{p3}212", [p3, $"{p3}2", $"{p3}21"]),
                Page($"{p3}213", [p3, $"{p3}2", $"{p3}21"]),
                Page($"{p3}221", [p3, $"{p3}2", $"{p3}22"]),
                Page($"{p3}222", [p3, $"{p3}2", $"{p3}22"]),
                Page($"{p3}231", [p3, $"{p3}2", $"{p3}23"]),
                Page($"{p3}232", [p3, $"{p3}2", $"{p3}23"]),
                Page($"{p3}233", [p3, $"{p3}2", $"{p3}23"]),
                Page($"{p3}241", [p3, $"{p3}2", $"{p3}24"]),
                Page($"{p3}242", [p3, $"{p3}2", $"{p3}24"]),
                Page($"{p3}243", [p3, $"{p3}2", $"{p3}24"]),
                Page($"{p3}311", [p3, $"{p3}3", $"{p3}31"]),
                Page($"{p3}312", [p3, $"{p3}3", $"{p3}31"]),
                Page($"{p3}313", [p3, $"{p3}3", $"{p3}31"]),
                Page($"{p3}314", [p3, $"{p3}3", $"{p3}31"]),
                Page($"{p3}321", [p3, $"{p3}3", $"{p3}32"]),
                Page($"{p3}322", [p3, $"{p3}3", $"{p3}32"]),
                Page($"{p3}331", [p3, $"{p3}3", $"{p3}33"]),
                Page($"{p3}332", [p3, $"{p3}3", $"{p3}33"]),
                Page($"{p3}333", [p3, $"{p3}3", $"{p3}33"]),
                Page($"{p3}341", [p3, $"{p3}3", $"{p3}34"]),
                Page($"{p3}342", [p3, $"{p3}3", $"{p3}34"]),
                Page($"{p3}343", [p3, $"{p3}3", $"{p3}34"]),
                Page($"{p3}344", [p3, $"{p3}3", $"{p3}34"]),
                Page($"{p3}411", [p3, $"{p3}4", $"{p3}41"]),
                Page($"{p3}412", [p3, $"{p3}4", $"{p3}41"]),
                Page($"{p3}421", [p3, $"{p3}4", $"{p3}42"]),
                Page($"{p3}422", [p3, $"{p3}4", $"{p3}42"]),
                Page($"{p3}431", [p3, $"{p3}4", $"{p3}43"]),
                Page($"{p3}432", [p3, $"{p3}4", $"{p3}43"]),
                Page($"{p3}433", [p3, $"{p3}4", $"{p3}43"]),
                Page($"{p3}441", [p3, $"{p3}4", $"{p3}44"]),
                Page($"{p3}442", [p3, $"{p3}4", $"{p3}44"]),
            ];
        }

        private List<PageTemplate> CreatePagesL4()
        {
            return
            [
                // Level 4
                Page($"{p2}1111", [p2, $"{p2}1", $"{p2}11", $"{p2}111"]),
                Page($"{p2}1112", [p2, $"{p2}1", $"{p2}11", $"{p2}111"]),
                Page($"{p2}1113", [p2, $"{p2}1", $"{p2}11", $"{p2}111"]),
                Page($"{p2}1114", [p2, $"{p2}1", $"{p2}11", $"{p2}111"]),
                Page($"{p2}1211", [p2, $"{p2}1", $"{p2}12", $"{p2}121"]),
                Page($"{p2}1212", [p2, $"{p2}1", $"{p2}12", $"{p2}121"]),
                Page($"{p2}1213", [p2, $"{p2}1", $"{p2}12", $"{p2}121"]),
                Page($"{p2}1221", [p2, $"{p2}1", $"{p2}12", $"{p2}122"]),
                Page($"{p2}1222", [p2, $"{p2}1", $"{p2}12", $"{p2}122"]),
                Page($"{p2}1223", [p2, $"{p2}1", $"{p2}12", $"{p2}122"]),
                Page($"{p2}1311", [p2, $"{p2}1", $"{p2}13", $"{p2}131"]),
                Page($"{p2}1312", [p2, $"{p2}1", $"{p2}13", $"{p2}131"]),
                Page($"{p2}1331", [p2, $"{p2}1", $"{p2}13", $"{p2}133"]),
                Page($"{p2}1332", [p2, $"{p2}1", $"{p2}13", $"{p2}133"]),
                Page($"{p2}1411", [p2, $"{p2}1", $"{p2}14", $"{p2}141"]),
                Page($"{p2}1412", [p2, $"{p2}1", $"{p2}14", $"{p2}141"]),
                Page($"{p2}1441", [p2, $"{p2}1", $"{p2}14", $"{p2}144"]),
                Page($"{p2}1442", [p2, $"{p2}1", $"{p2}14", $"{p2}144"]),
                Page($"{p2}1443", [p2, $"{p2}1", $"{p2}14", $"{p2}144"]),
                Page($"{p2}2131", [p2, $"{p2}2", $"{p2}21", $"{p2}213"]),
                Page($"{p2}2132", [p2, $"{p2}2", $"{p2}21", $"{p2}213"]),
                Page($"{p2}2133", [p2, $"{p2}2", $"{p2}21", $"{p2}213"]),
                Page($"{p2}2311", [p2, $"{p2}2", $"{p2}23", $"{p2}231"]),
                Page($"{p2}2312", [p2, $"{p2}2", $"{p2}23", $"{p2}231"]),
                Page($"{p2}2313", [p2, $"{p2}2", $"{p2}23", $"{p2}231"]),
                Page($"{p2}2314", [p2, $"{p2}2", $"{p2}23", $"{p2}231"]),
            ];
        }

        private static PageTemplate Page(string name, List<string> parents)
            => new()
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
