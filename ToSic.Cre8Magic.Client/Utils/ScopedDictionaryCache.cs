namespace ToSic.Cre8magic.Utils;

internal class ScopedDictionaryCache<T>() : Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);