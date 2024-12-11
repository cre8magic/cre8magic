using ToSic.Cre8magic.Spells.Debug;

namespace ToSic.Cre8magic.Spells.Internal.Debug;

internal interface IHasDebugSettings
{
    /// <summary>
    /// Debug settings for anything that can configure show/hide of debug
    /// </summary>
    MagicDebugSettings? Debug { get; }
}