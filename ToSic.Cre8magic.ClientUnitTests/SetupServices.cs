using Microsoft.Extensions.DependencyInjection;

namespace ToSic.Cre8magic.ClientUnitTests;

/// <summary>
/// Helper to prepare the services.
/// </summary>
internal class SetupServices
{
    /// <summary>
    /// Prepare the services.
    /// </summary>
    /// <returns>The service provider.</returns>
    public static IServiceProvider PrepareServices()
    {
        var serviceCollection = new ServiceCollection();
        var startup = new Startup();
        startup.ConfigureServices(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }
}