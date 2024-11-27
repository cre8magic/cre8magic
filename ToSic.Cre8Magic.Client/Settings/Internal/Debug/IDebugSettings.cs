namespace ToSic.Cre8magic.Settings.Internal.Debug;

public interface IDebugSettings
{
    internal MagicSettingsCatalog? Catalog { get; set; }

    internal bool? DebugThis { get; set; }
}