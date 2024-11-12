namespace ToSic.Cre8magic.Settings.Internal;

public interface IHasSettingsExceptions
{
    public bool HasExceptions => Exceptions?.Any() ?? false;

    List<Exception> Exceptions { get; }
}