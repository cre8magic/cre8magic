namespace ToSic.Cre8magic.Settings.Internal.Journal;

public record DataWithJournal<T>(T Data, Journal Journal);


internal record Data2WithJournal<T1, T2>(T1 Data, T2 Data2, Journal Journal): DataWithJournal<T1>(Data, Journal);

internal record Data3WithJournal<T1, T2, T3>(T1 Data, T2 Data2, T3 Data3, Journal Journal) : Data2WithJournal<T1, T2>(Data, Data2, Journal);