using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Simple Object containing a setting to be used when a value is on or off.
/// Typically used to specify strings to place in classes when something is active, is home, etc.
/// </summary>
public class PairOnOff: ICanClone<PairOnOff>
{
    /// <summary>
    /// Empty constructor for JSON serialization
    /// </summary>
    public PairOnOff() {}

    public PairOnOff(string? on, string? off = null)
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

    public PairOnOff CloneWith(PairOnOff? priority, bool forceCopy = false) =>
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
    public static string? Get(this PairOnOff? pair, bool? isOn) => isOn == true ? pair?.On : pair?.Off;
}