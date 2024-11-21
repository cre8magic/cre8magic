using Oqtane.Models;
using ToSic.Cre8magic.Utils;
using static ToSic.Cre8magic.MagicTokens;

namespace ToSic.Cre8magic.Tokens;

internal class ModuleTokens(Module module) : ITokenReplace
{
    private const string NameIdConstant = nameof(ModuleTokens);

    public string NameId => NameIdConstant;

    /// <summary>
    /// Standard replace for strings
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string? Parse(string? value)
    {
        if (!value.HasValue()) return value;
        var mod = value!
                .Replace(ModuleId, $"{module.ModuleId}")
                .Replace(ModuleControlName, () => NamespaceParts[^1])
                .Replace(ModuleNamespace, () => string.Join('.', NamespaceParts[..^1]))
            ;
        return mod;
    }

    private string[] NamespaceParts => module.ModuleType.Split(',')[0].Split('.');
}