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

    public List<StartNodeRule> GetStartNodeRules(string? value)
    {
        if (_cache.TryGetValue(value ?? "", out var result))
            return result;
        _cache[value ?? ""] = result = GenerateStartNodeRules(value);
        return result;
    }

    private readonly Dictionary<string, List<StartNodeRule>> _cache = new();

    public List<StartNodeRule> GenerateStartNodeRules(string? value)
    {
        var l = Log.Fn<List<StartNodeRule>>($"{nameof(value)}: '{value}'");

        if (!value.HasText())
            return l.Return([], "no value, empty list");

        var parts = value.Split(',')
            .Select(s => s.Trim())
            .Where(s => s.HasText())
            .ToList();

        var result = parts
            .Select(startCode =>
            {
                var processed = startCode;

                // Check if starting with a Page ID
                var idMatch = FindPageId().Match(processed);
                var id = idMatch.Success
                    ? int.TryParse(idMatch.Groups["page"].Value, out var idTemp) ? idTemp : 0
                    : 0;

                if (idMatch.Success)
                    processed = idMatch.Groups["rest"].Value;

                // Start checking leading ".." or "." and remove them
                var fromParent = processed.StartsWith("..");
                var fromCurrent = !fromParent && processed.StartsWith('.');
                if (fromParent || fromCurrent)
                    processed = processed.TrimStart('.');

                // Check for trailing "!" to force the page (only for PageId)
                var force = processed.StartsWith(PageForced);
                if (force)
                    processed = processed.TrimEnd(PageForced);

                // Check starting "/" or "//"
                var fromRoot = !(fromCurrent || fromParent || id != 0) && processed.StartsWith(MagicMenuSettings.StartPageRootSlash);

                // The string processed now should start with a number, which we should extract using regex
                var levelMatch = FindLevelNumber().Match(processed);
                var defLevel = fromParent ? -1 : fromCurrent ? 0 : 1;
                var level = levelMatch.Success
                    ? int.TryParse(levelMatch.Groups["level"].Value, out var lvl) ? lvl : defLevel
                    : defLevel;

                // Keep rest
                // todo...- if it just started with a "/", then the rest still contains it, so clean up
                if (levelMatch.Success)
                    processed = levelMatch.Groups["processed"].Value;

                // If we still have "//" then it was without a level number, so default to 0
                if (processed.StartsWith("" + MagicMenuSettings.StartPageRootSlash + MagicMenuSettings.StartPageRootSlash))
                {
                    level = 1;
                    processed = processed.TrimStart(MagicMenuSettings.StartPageRootSlash);
                }


                // If we now have "//" as the from - root, or as the left - over from current/ parent, it could have a trailing level number
                var endWithSingleSlash = processed.StartsWith(MagicMenuSettings.StartPageRootSlash);

                processed = processed.TrimStart(MagicMenuSettings.StartPageRootSlash);

                // Count + characters to determine level
                var depth = processed.TakeWhile(c => c == '+').Count() + 1;
                processed = processed.TrimStart('+');

                var modeInfo = id != default
                    ? StartMode.PageId
                    : fromRoot
                        ? StartMode.Root
                        : fromCurrent || fromParent
                            ? StartMode.Current
                            : StartMode.Unknown;

                return new StartNodeRule
                {
                    Id = id,
                    Force = force,
                    Depth = depth,
                    ShowChildren = !fromRoot && endWithSingleSlash,
                    Level = level,
                    ModeInfo = modeInfo,
                    Raw = startCode
                };
            })
            .ToList();

        return l.ReturnAndKeepData(result, result.Count.ToString());
    }

    /// <summary>
    /// Check for leading "//" and likely level number
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^//(?<level>\d+)(?<rest>.*)")]
    private static partial Regex FindLevelNumber();

    [GeneratedRegex(@"^(?<page>\d+)(?<rest>.*)")]
    private static partial Regex FindPageId();

}