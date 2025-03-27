namespace ToSic.Cre8magic.Settings.Internal.Debug;

public interface IDebugSettings
{
    internal MagicBook? Book { get; set; }

    internal bool? DebugThis { get; set; }
}