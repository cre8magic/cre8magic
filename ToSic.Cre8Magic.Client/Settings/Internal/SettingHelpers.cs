using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Utils.Internal;

namespace ToSic.Cre8magic.Settings.Internal;

internal class SettingHelpers
{
    internal static int PickFirstNonZeroInt(int?[] values)
        => values.FirstOrDefault(v => v != null && v != 0) ?? 0;

    public static string RandomLongId()
        => new Random().Next(100000, 1000000).ToString();

    public static string RandomLongId(string? id)
        => id.HasText()
            ? id
            : RandomLongId();
}