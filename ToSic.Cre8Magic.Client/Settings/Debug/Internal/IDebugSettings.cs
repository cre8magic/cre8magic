namespace ToSic.Cre8magic.Settings.Debug.Internal;

public interface IDebugSettings
{
    internal MagicBook? Book { get; set; }

    internal bool? DebugThis { get; set; }
}