using Oqtane.Models;
using Oqtane.Modules;

namespace ToSic.Module.PageTemplates
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "PageTemplates",
            Description = "generate pages for test",
            Version = "1.0.0",
            ServerManagerType = "ToSic.Module.PageTemplates.Manager.PageTemplatesManager, ToSic.Module.PageTemplates.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "ToSic.Module.PageTemplates.Shared.Oqtane",
            PackageName = "ToSic.Module.PageTemplates" 
        };
    }
}
