using Microsoft.Extensions.DependencyInjection;

namespace ToSic.Cre8magic.Internal;

internal class MagicLazy<T>(IServiceProvider provider) : Lazy<T>(provider.GetRequiredService<T>)
    where T : class;