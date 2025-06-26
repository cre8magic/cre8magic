using Microsoft.Extensions.DependencyInjection;

namespace ToSic.Cre8magic.Internal;

/// <summary>
/// A simple Lazy implementation.
/// We want to use our own object, to not pollute the Oqtane DI since it probably has its own Lazy implementation.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="provider"></param>
internal class MagicLazy<T>(IServiceProvider provider) : Lazy<T>(provider.GetRequiredService<T>)
    where T : class;