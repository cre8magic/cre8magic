using Microsoft.Extensions.DependencyInjection;

namespace ToSic.Cre8magic.Internal.Startup;

/// <summary>
/// Class to connect Oqtane with the Service Configuration.
/// </summary>
public class OqtaneClientStartup : Oqtane.Services.IClientStartup
{
    /// <summary>
    /// Register Services
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services) => services.AddCre8magic();
}
