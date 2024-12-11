using System.Text.RegularExpressions;
using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Menus.Internal.Nodes;

/// <summary>
/// Helper to find various pages based on rules such as
/// * "27" - the page 27
/// * "27,28" - the pages 27 and 28
/// * "/" - the top-level pages
/// * "." - the current page
/// * ".." - the parent of the current page
/// * "./" - the children of the current page
/// * "27/" - the children of page 27
/// * "../" - the children of the parent of the current page
/// </summary>
internal partial class NodeRuleParser(LogRoot logRoot)
{
    public const char PageForced = '!';

    internal Log Log { get; } = logRoot.GetLog("RuleParser");

    public List<PagesPickRule> GetStartNodeRules(string? value)
    {
        if (_cache.TryGetValue(value ?? "", out var result))
            return result;
        _cache[value ?? ""] = result = GenerateStartNodeRules(value);
        return result;
    }

    private readonly Dictionary<string, List<PagesPickRule>> _cache = new();

    public List<PagesPickRule> GenerateStartNodeRules(string? raw)
    {
        var l = Log.Fn<List<PagesPickRule>>($"{nameof(raw)}: '{raw}'");

        if (!raw.HasText())
            return l.Return([], "no value, empty list");

        var parts = raw.Split(',')
            .Select(s => s.Trim())
            .Where(s => s.HasText())
            .ToList();

        l.A($"Parts: {parts.Count}");

        var result = parts.Select(startCode =>
            {
                l.A($"Parsing Rule: '{startCode}'");

                // Check if starting with a Page ID
                var idMatch = FindPageId().Match(startCode);
                var id = idMatch.Success
                    ? int.TryParse(idMatch.Groups["page"].Value, out var idTemp) ? idTemp : 0
                    : 0;

                var afterId = idMatch.Success
                    ? idMatch.Groups["rest"].Value
                    : startCode;

                // Start checking leading ".." or "." and remove them
                var fromParent = afterId.StartsWith(MagicMenuSettings.StartPageParent);
                var fromCurrent = !fromParent && afterId.StartsWith(MagicMenuSettings.StartPageCurrent);
                var afterParentCurrent = fromParent || fromCurrent
                    ? afterId.TrimStart('.')
                    : afterId;

                // Check for trailing "!" to force the page (only for PageId)
                var force = afterParentCurrent.StartsWith(PageForced);
                var afterForce = force
                    ? afterParentCurrent.TrimStart(PageForced)
                    : afterParentCurrent;

                // Check starting "/" or "//" if other from... did not match
                var fromNotYetSet = !fromCurrent && !fromParent && id == 0;
                var fromRoot = fromNotYetSet && afterId.StartsWith(MagicMenuSettings.StartPageRootSlash);
                var useRootBreadcrumb = fromNotYetSet && afterId.StartsWith(MagicMenuSettings.DoubleSlash);

                l.A($"Starts at {nameof(fromCurrent)}: {fromCurrent}; {nameof(fromParent)}: {fromParent}; {nameof(fromRoot)}: {fromRoot}; {nameof(force)}: {force}");
                l.A($"Progress: '{startCode}' > '{afterId}' > '{afterParentCurrent}' > '{afterForce}'");

                // The string processed now should start with a number, which we should extract using regex
                var levelMatch = FindLevelNumberAfterDoubleSlash().Match(afterForce);
                var defLevel = fromParent ? -1 : fromCurrent ? 0 : 1;
                var likelyLevel = levelMatch.Success
                    ? int.TryParse(levelMatch.Groups["level"].Value, out var lvl) ? lvl : defLevel
                    : defLevel;

                // Keep rest
                // todo...- if it just started with a "/", then the rest still contains it, so clean up
                var afterLevelMatch = levelMatch.Success
                    ? levelMatch.Groups["rest"].Value
                    : afterForce;

                // If we still have "//" then it was without a level number, so default to 0
                var useBreadcrumb = levelMatch.Success || useRootBreadcrumb || afterLevelMatch.StartsWith(MagicMenuSettings.DoubleSlash);
                var level = (useRootBreadcrumb && likelyLevel < 0) || (useBreadcrumb && likelyLevel == 0)
                    ? 1
                    : likelyLevel;

                l.A($"{nameof(likelyLevel)}: {likelyLevel}; {nameof(useBreadcrumb)}: {useBreadcrumb}; {nameof(level)}: {level}");
                l.A($"Progress: '{afterForce}' > '{afterLevelMatch}'");
                

                var afterDoubleSlash = useBreadcrumb
                    ? afterLevelMatch.TrimStart(MagicMenuSettings.StartPageRootSlash)
                    : afterLevelMatch;

                // If we now have "//" as the from - root, or as the left - over from current/ parent, it could have a trailing level number
                var endWithSingleSlash = afterDoubleSlash.StartsWith(MagicMenuSettings.StartPageRootSlash);

                var afterSlash = afterDoubleSlash.TrimStart(MagicMenuSettings.StartPageRootSlash);

                // Count + characters to determine level
                var depth = afterSlash.TakeWhile(c => c == '+').Count() + 1;
                var afterDepth = afterSlash.TrimStart('+');

                var modeInfo = id != default
                    ? StartMode.PageId
                    : fromRoot
                        ? StartMode.Root
                        : fromCurrent || fromParent
                            ? StartMode.Current
                            : StartMode.Unknown;

                var levelFromRootNeverNegative = !fromRoot
                    ? level
                    : level > 0
                        ? level
                        : 1;

                var probablyShowChildren = !fromRoot && endWithSingleSlash;
                (int Level, bool ShowChildren) levelChildrenPair = useBreadcrumb
                    ? level < 1
                        ? (level, true)
                        : (level, probablyShowChildren)
                    : (levelFromRootNeverNegative, probablyShowChildren);

                return new PagesPickRule
                {
                    Id = id,
                    Force = force,
                    Depth = depth,
                    ShowChildren =
                        levelChildrenPair.ShowChildren,
                    Level = levelChildrenPair.Level,
                    ModeInfo = modeInfo,
                    Raw = startCode
                };
            })
            .ToList();

        return l.ReturnAndKeepData(result, $"Rules: {result.Count}");
    }

    /// <summary>
    /// Check for leading "//" and likely level number
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^//(?<level>-?\d+)(?<rest>.*)")]
    private static partial Regex FindLevelNumberAfterDoubleSlash();

    [GeneratedRegex(@"^(?<page>\d+)(?<rest>.*)")]
    private static partial Regex FindPageId();

}