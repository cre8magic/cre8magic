namespace ToSic.Cre8magic.Settings.Internal;

internal class SpellHelpers
{
    internal static int PickFirstNonZeroInt(int?[] values) => values.FirstOrDefault(v => v != null && v != 0) ?? 0;
}