namespace ToSic.Cre8magic.Settings.Internal.Debug;

public interface IDebugSettings
{
    internal MagicSpellsBook? Book { get; set; }

    internal bool? DebugThis { get; set; }
}