using Oqtane.Models;
using static ToSic.Cre8magic.Client.MagicTokens;

namespace ToSic.Cre8magic.Client.Tokens;

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