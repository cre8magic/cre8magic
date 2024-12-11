namespace ToSic.Cre8magic.Settings.Internal;

public static class PairOnOffExtensions
{
    /// <summary>
    /// Null-safe pair access
    /// </summary>
    /// <param name="pair"></param>
    /// <param name="isOn"></param>
    /// <returns></returns>
    public static string? Get(this MagicSettingOnOff? pair, bool? isOn) => isOn == true ? pair?.On : pair?.Off;
}