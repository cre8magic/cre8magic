namespace ToSic.Cre8magic.Settings.Internal.Journal;

public record Journal(List<string> Messages, List<Exception> Exceptions)
{
    public Journal(): this([], []) { }

    public Journal With(Journal journal) => new(Messages.Concat(journal.Messages).ToList(), Exceptions.Concat(journal.Exceptions).ToList());

    public Journal With(string message) => new(Messages.Append(message).ToList(), Exceptions);

    public Journal With(Exception exception) => new(Messages, Exceptions.Append(exception).ToList());

    public Journal With(List<string> messages) => new(Messages.Concat(messages).ToList(), Exceptions);

    public Journal With(List<Exception> exceptions) => new(Messages, Exceptions.Concat(exceptions).ToList());
}
