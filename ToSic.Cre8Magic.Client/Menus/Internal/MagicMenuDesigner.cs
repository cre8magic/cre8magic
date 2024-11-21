using System.Collections;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal;

/// <summary>
/// Special helper to provide Css classes to menus
/// </summary>
public class MagicMenuDesigner : IMagicPageDesigner
{
    internal MagicMenuDesigner(MagicMenuContextWip context)
    {
        Settings = context.Settings ?? throw new ArgumentNullException(nameof(context), $"{nameof(context.Settings)} null");

        DesignSettingsList = [Settings.DesignSettings!];

        // TODO: REACTIVATE, PROBABLY ON ALL MENU DESIGNERS?
        Log = context.Settings.Debug?.Detailed == true ? context.LogRoot.GetLog("MenuDesigner") : null;
    }
    private MagicMenuSettings Settings { get; }

    // TODO: unclear why this is a list, it can only contain one...?
    internal List<Dictionary<string, MagicMenuDesignSettings>> DesignSettingsList { get; }

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

    private List<MagicMenuDesignSettings> ConfigsForTag(string tag) =>
        DesignSettingsList
            .Select(c => c.FindInvariant(tag))
            .Where(c => c is not null)
            .ToList()!;

    private List<string?> TagClasses(IMagicPage page, IReadOnlyCollection<MagicMenuDesignSettings> configs)
    {
        var classes = new List<string?>();

        void AddIfAny(IEnumerable<string?> maybeAdd)
        {
            var additions = maybeAdd.Where(v => v != null).ToList();
            if (additions.Any()) classes.AddRange(additions);
        }

        AddIfAny(configs.Select(c => c.Classes));
        AddIfAny(configs.Select(c => c.Classes));
        AddIfAny(configs.Select(c => c.IsActive.Get(page.IsActive)));
        AddIfAny(configs.Select(c => c.HasChildren.Get(page.HasChildren)));
        AddIfAny(configs.Select(c => c.IsDisabled.Get(!page.IsClickable)));
        // TODO: this needs a cast to MagicMenuPage, should be improved; probably InBreadcrumb could become an official property?
        AddIfAny(configs.Select(c => c.InBreadcrumb.Get(page.IsInBreadcrumb)));

        // See if there are any css for this level or for not-specified levels
        var levelCss = configs
            .Select(c => c.ByLevel == null
                ? null
                : c.ByLevel.TryGetValue(page.MenuLevel, out var levelClasses)
                    ? levelClasses
                    : c.ByLevel.TryGetValue(MagicTokens.ByLevelOtherKey, out var levelClassesDefault)
                        ? levelClassesDefault
                        : null
            );
        AddIfAny(levelCss);

        return classes;
    }

    private string ListToClasses(IEnumerable<string?> original)
        => string.Join(" ", original.Where(s => !s.IsNullOrEmpty())).Replace("  ", " ");
}