using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Analytics.Internal;
using ToSic.Cre8magic.Breadcrumb;
using ToSic.Cre8magic.Breadcrumb.Internal;
using ToSic.Cre8magic.Languages.Internal;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.Menus.Internal;
using ToSic.Cre8magic.PageContext;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Services.Internal;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Sources;
using ToSic.Cre8magic.Settings.Json;
using ToSic.Cre8magic.Themes;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Users;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic;

public class Startup : Oqtane.Services.IClientStartup
{
    /// <summary>
    /// Register Services
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        // All these Settings etc. should be scoped, so they don't have to reload for each click
        services.TryAddScoped<IMagicSettingsService, MagicSettingsService>();

        // Services used by SettingsService, which is scoped, so the dependencies can be normal transient
        services.TryAddTransient<MagicSettingsLoader>();
        services.TryAddTransient<MagicSettingsJsonService>();

        services.TryAddTransient<IMagicLanguageService, MagicLanguageService>();
        services.TryAddTransient<IMagicThemeService, MagicThemeService>();
        services.TryAddTransient<IMagicPageContextService, MagicPageContextService>();
        services.TryAddTransient<IMagicUserService, MagicUserService>();
        services.TryAddTransient<IMagicAnalyticsService, MagicAnalyticsService>();
        services.TryAddTransient<IMagicBreadcrumbService, MagicBreadcrumbService>();

        services.TryAddTransient<MagicThemeJsServiceTest>();

        // Logic parts for Controls
        services.TryAddTransient<MagicPageEditService>();

        // Analytics - new in 0.0.2
        services.TryAddTransient<MagicAnalyticsService>();

        services.TryAddTransient<IMagicMenuService, MagicMenuService>();

        //services.TryAddTransient<MagicPageService>(); // Can't DI because of PageState dependency that breaks Oqtane 
        //services.TryAddTransient<MagicMenuTree>(); // Can't DI because of PageState dependency that breaks Oqtane 

        // WIP v0.02.00
        services.TryAddTransient<IMagicPageService, MagicPageService>();
        services.TryAddTransient<IMagicFactoryWip, MagicFactoryWip>();

        // Main Settings Provider, scoped, to be used on two following interfaces
        services.TryAddScoped<MagicSettingsProviders>();
        services.TryAddTransient<IMagicSettingsProviders>(s => s.GetService<MagicSettingsProviders>());
        services.AddTransient<IMagicSettingsSource>(s => s.GetService<MagicSettingsProviders>());

        services.TryAddScoped(typeof(ScopedDictionaryCache<>));

        // Json sources
        services.TryAddScoped<MagicSettingsJsonService>();
        services.AddScoped<IMagicSettingsSource, MagicSettingsSourceJson>();
        services.AddTransient<IMagicSettingsSource, MagicSettingsSourcePackageDefaults>();
    }
}
