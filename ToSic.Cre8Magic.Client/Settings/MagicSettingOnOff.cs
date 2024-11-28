using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Special Setting containing a value to be used when a state is on or off.
/// Typically used to specify strings to place in classes when something:
/// * is active
/// * is home
/// * etc.
/// </summary>
public class MagicSettingOnOff: ICanClone<MagicSettingOnOff>
{
    /// <summary>
    /// Empty constructor for JSON serialization
    /// </summary>
    [PrivateApi]
    public MagicSettingOnOff() {}

    public MagicSettingOnOff(string? on, string? off = null)
    {
        On = on;
        Off = off;
    }

    /// <summary>
    /// Value / Class to add if the setting is on
    /// </summary>
    public string? On { get; init; }

    /// <summary>
    /// Value / Class to add if the setting is off
    /// </summary>
    public string? Off { get; init; }

    MagicSettingOnOff ICanClone<MagicSettingOnOff>.CloneUnder(MagicSettingOnOff? priority, bool forceCopy = false) =>
        new()
        {
            On = priority?.On ?? On,
            Off = priority?.Off ?? Off
        };
}


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