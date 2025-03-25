using Oqtane.Models;
using Oqtane.Shared;

namespace ToSic.Cre8magic.Themes;

public class MagicResourcesDb
{
    internal static Resource ResourceInteropJs = new()
    {
        ResourceType = ResourceType.Script,
        Url = $"_content/{MagicConstants.PackageId}/interop.js",
        LoadBehavior = ResourceLoadBehavior.Once,
        Type = "module",
    };

    internal static Resource ResourceAmbientJs = new()
    {
        ResourceType = ResourceType.Script,
        Url = $"_content/{MagicConstants.PackageId}/ambient.js",
        LoadBehavior = ResourceLoadBehavior.Once,
    };

    internal static Resource ResourceAmbientCssBs5 = new()
    {
        ResourceType = ResourceType.Stylesheet,
        Url = $"_content/{MagicConstants.PackageId}/styles-bs5.css",
        LoadBehavior = ResourceLoadBehavior.Once,
    };

    /// <summary>
    /// Gets the common resources that should be included in all Cre8magic themes.
    /// </summary>
    /// <returns>A list of common resources</returns>
    public static readonly List<Resource> CommonResources =
    [
        ResourceInteropJs,
        ResourceAmbientJs,
        ResourceAmbientCssBs5,
    ];

}
