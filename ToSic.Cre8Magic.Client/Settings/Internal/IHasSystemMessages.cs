namespace ToSic.Cre8magic.Settings.Internal;

public interface IHasSystemMessages
{
    public bool HasExceptions => Exceptions?.Any() ?? false;

    List<Exception> Exceptions { get; }
}