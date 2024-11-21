namespace ToSic.Cre8magic.Settings.Internal;

internal record DataWithJournal<T>(T Data, List<string> Journal);


internal record Data2WithJournal<T1, T2>(T1 Data, T2 Data2, List<string> Journal): DataWithJournal<T1>(Data, Journal);

internal record Data3WithJournal<T1, T2, T3>(T1 Data, T2 Data2, T3 Data3, List<string> Journal) : Data2WithJournal<T1, T2>(Data, Data2, Journal);