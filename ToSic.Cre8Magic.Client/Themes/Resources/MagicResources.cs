using Oqtane.Models;
using Oqtane.Shared;

namespace ToSic.Cre8magic.Themes;

public class MagicResources
{
    public static List<Resource> GetAll() => MagicResourcesDb.CommonResources;

    public static List<Resource> GetResources(MagicResourcesFilter? filter = null)
    {
        if (filter == null)
            return MagicResourcesDb.CommonResources;

        // If we have flags, then make sure we only choose these resources
        var list = filter.ForControls.HasFlag(MagicControls.All)
            ? MagicResourcesDb.CommonResources
            : MagicResourcesDb.ByControl
                .Where(pair => filter.ForControls.HasFlag(pair.Key))
                .SelectMany(pair => pair.Value)
                .Distinct()
                .ToList();

        // Filter by resource type
        list = filter.ResourceType == null
            ? list
            : list
                .Where(r => r.ResourceType == filter?.ResourceType)
                .ToList();


        return list;
    }

    /// <summary>
    /// Deduplicate resources - not sure if this is useful, temporary for now.
    /// </summary>
    /// <param name="themeResources">The theme-specific resources</param>
    /// <returns>Merged resources list with common resources first</returns>
    [PrivateApi]
    public static List<Resource>? Deduplicate(List<Resource>? themeResources)
    {
        if (themeResources == null || !themeResources.Any())
            return themeResources;

        // Deduplicate
        var deduplicated = themeResources
            .DistinctBy(r => r.Url.ToLowerInvariant())
            .ToList();

        return deduplicated;
    }

}