namespace ToSic.Cre8magic.Spells.Internal.Debug;

public interface IDebugSettings
{
    internal MagicSpellsBook? Book { get; set; }

    internal bool? DebugThis { get; set; }
}