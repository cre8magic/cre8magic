namespace ToSic.Cre8magic.Settings.Providers;

/// <summary>
/// Provider to give settings.
///
/// It is scoped, so anything added to it - typically in the Theme,
/// will be available in all other components.
/// </summary>
public interface IMagicSettingsProvider
{
    public void Reset();
    IMagicSettingsProvider Provide(MagicBook book);

    void Add(MagicChapter chapter);
}