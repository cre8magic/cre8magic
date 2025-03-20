using Oqtane.Models;

namespace ToSic.Cre8magic.Internal;

/// <summary>
/// Manages resources for Cre8magic themes, automatically providing static web assets as resources.
/// </summary>
public static class MagicResourceManager
{
    /// <summary>
    /// Merges common resources with theme-specific resources.
    /// </summary>
    /// <param name="themeResources">The theme-specific resources</param>
    /// <returns>Merged resources list with common resources first</returns>
    public static List<Resource> GetResources(List<Resource>? themeResources)
    {
        List<Resource> result = [ ..MagicThemePackage.CommonResources ];

        if (themeResources == null || !themeResources.Any()) return result;

        // Add theme resources, but skip any that have the same URL to avoid duplicates
        foreach (var resource in themeResources.Where(resource => !result.Any(r => r.Url.Equals(resource.Url, StringComparison.OrdinalIgnoreCase))))
            result.Add(resource);

        return result;
    }
}