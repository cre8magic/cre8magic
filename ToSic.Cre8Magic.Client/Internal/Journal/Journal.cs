namespace ToSic.Cre8magic.Internal.Journal;

public record Journal(List<string> Messages, List<Exception> Exceptions)
{
    public Journal(): this([], []) { }

    public Journal With(Journal journal) => new(Messages.Concat(journal.Messages).ToList(), Exceptions.Concat(journal.Exceptions).ToList());

    public Journal With(string message) => this with { Messages = Messages.Append(message).ToList() };

    public Journal With(Exception exception) => this with { Exceptions = Exceptions.Append(exception).ToList() };

    public Journal With(List<string> messages) => this with { Messages = Messages.Concat(messages).ToList() };

    public Journal With(List<Exception> exceptions) => this with { Exceptions = Exceptions.Concat(exceptions).ToList() };
}
