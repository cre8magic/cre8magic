﻿using System.Collections;
using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Internal.Logging;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings.Values.Internal;
using ToSic.Cre8magic.Tailors;
using ToSic.Cre8magic.Utils;


namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Special helper to provide Css classes to menus
/// </summary>
public class MagicMenuTailor : IMagicPageTailor
{
    internal MagicMenuTailor(MagicMenuWorkContext workContext)
    {
        Settings = workContext.Settings ?? throw new ArgumentNullException(nameof(workContext), $"{nameof(workContext.Settings)} null");

        DesignSettingsList = [Settings.GetStable().Blueprint];

        // TODO: REACTIVATE, PROBABLY ON ALL MENU DESIGNERS?
        Log = workContext.Settings.Debug?.Detailed == true ? workContext.LogRoot.GetLog(nameof(MagicMenuTailor)) : null;
    }
    private MagicMenuSettings Settings { get; }

    // TODO: unclear why this is a list, it can only contain one...?
    internal List<MagicMenuBlueprint> DesignSettingsList { get; }

    private ILog? Log { get; }

    public string Classes(string tag, IMagicPage page)
    {
        var l = Log.Fn<string>($"{nameof(tag)}: {tag}, page: {page.Id} \"{page.Name}\"");
        var configsForTag = ConfigsForTag(tag);
        var result = configsForTag.Any()
            ? ListToClasses(TagClasses(page, configsForTag))
            : "";
        return l.ReturnAndLog(result);
    }

    public string Value(string key, IMagicPage page)
    {
        var l = Log.Fn<string>(key);
        var configsForKey = ConfigsForTag(key)
            .Select(c => c.Value)
            .Where(v => v.HasValue())
            .ToList();

        return l.ReturnAndLog(string.Join(" ", configsForKey));
    }

    private List<MagicBlueprintPart> ConfigsForTag(string tag) =>
        DesignSettingsList
            .Select(c => c.GetStable().Parts.FindInvariant(tag))
            .Where(c => c is not null)
            .ToList()!;

    private static List<string?> TagClasses(IMagicPage page, IReadOnlyCollection<MagicBlueprintPart> configs)
    {
        var classes = new List<string?>();

        AddIfAny(configs.Select(c => c.Classes));
        AddIfAny(configs.Select(c => c.IsActive.Get(page.IsActive)));
        AddIfAny(configs.Select(c => c.HasChildren.Get(page.HasChildren)));
        AddIfAny(configs.Select(c => c.IsDisabled.Get(!page.IsClickable)));
        AddIfAny(configs.Select(c => c.InBreadcrumb.Get(page.IsInBreadcrumb)));

        // See if there are any css for this level or for not-specified levels
        var levelCss = configs
            .Select(c => c.ByLevel == null
                ? null
                : c.ByLevel.TryGetValue(page.MenuLevel, out var levelClasses)
                    ? levelClasses
                    : c.ByLevel.GetValueOrDefault(MagicTokens.ByLevelOtherKey)
            );
        AddIfAny(levelCss);

        return classes;

        void AddIfAny(IEnumerable<string?> maybeAdd)
        {
            var additions = maybeAdd.Where(v => v != null).ToList();
            if (additions.Any()) classes.AddRange(additions);
        }
    }

    private string ListToClasses(IEnumerable<string?> original)
        => string.Join(" ", original.Where(s => !s.IsNullOrEmpty())).Replace("  ", " ");
}