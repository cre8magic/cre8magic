namespace ToSic.Cre8magic.Settings.Internal;

internal record DataWithJournal<T>(T Data, List<string> Journal);