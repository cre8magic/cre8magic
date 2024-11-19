namespace ToSic.Cre8magic.Settings.Internal.Logging;

public record SettingsLogs(List<Exception>? Exceptions) : IHasSystemMessages
{
    public bool HasExceptions => Exceptions?.Any() ?? false;
}