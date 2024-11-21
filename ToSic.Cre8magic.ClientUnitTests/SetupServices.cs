using Microsoft.Extensions.DependencyInjection;

namespace ToSic.Cre8magic.ClientUnitTests;

/// <summary>
/// Helper to prepare the services.
/// </summary>
internal static class SetupServices
{
    public static IServiceCollection Start() =>
        new ServiceCollection();

    public static IServiceProvider Finish(this IServiceCollection collection) =>
        collection.BuildServiceProvider();


    public static IServiceCollection AddStandardLogging(this IServiceCollection collection) =>
        collection.AddLogging();
}