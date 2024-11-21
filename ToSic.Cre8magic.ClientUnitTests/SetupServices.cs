using Microsoft.Extensions.DependencyInjection;
using ToSic.Cre8magic.Internal.Startup;

namespace ToSic.Cre8magic.ClientUnitTests;

/// <summary>
/// Helper to prepare the services.
/// </summary>
internal static class SetupServices
{
    public static IServiceCollection Start() => new ServiceCollection();

    public static IServiceProvider Finish(this IServiceCollection collection) => collection.BuildServiceProvider();

    /// <summary>
    /// Prepare the services.
    /// </summary>
    /// <returns>The service provider.</returns>
    public static IServiceCollection PrepareServices(this IServiceCollection serviceCollection)
    {
        var startup = new OqtaneClientStartup();
        startup.ConfigureServices(serviceCollection);
        return serviceCollection;
    }

    public static IServiceCollection AddStandardLogging(this IServiceCollection collection)
    {
        return collection.AddLogging();
    }
}