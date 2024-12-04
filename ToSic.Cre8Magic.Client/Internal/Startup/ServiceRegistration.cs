using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToSic.Cre8magic.Act;
using ToSic.Cre8magic.Act.Internal;
using ToSic.Cre8magic.Analytics.Internal;
using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Internal.JsInterops.Internal;
using ToSic.Cre8magic.Languages.Internal;
using ToSic.Cre8magic.Links;
using ToSic.Cre8magic.Menus.Internal;
using ToSic.Cre8magic.PageContexts.Internal;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal.Sources;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Settings.Internal.Providers;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.UserLogins.Internal;
using ToSic.Cre8magic.Users;
using ToSic.Cre8magic.Users.Internal;
using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Containers.Internal;
using ToSic.Cre8magic.Links.Internal;

// ReSharper disable InconsistentNaming

namespace ToSic.Cre8magic.Internal.Startup;

/// <summary>
/// Standalone class to register all services for Cre8Magic.
///
/// Done like this, so unit tests can better choose which parts to register.
/// </summary>
public static class ServiceRegistration
{
    public static IServiceCollection AddCre8magic(this IServiceCollection services)
    {
        services
            .AddCre8magicFoundation()
            .AddCre8magicSettingsCore()
            .AddCre8magicSettingsSourceProvider()
            .AddCre8magicSettingsSourcesPackage()
            .AddCre8magicMainParts()
            .AddCre8magicJsLayer()
            .AddOqtaneWorkarounds();

        return services;
    }

    public static IServiceCollection AddCre8magicFoundation(this IServiceCollection services)
    {
        // Infrastructure: Lazy Services
        services.TryAddTransient(typeof(MagicLazy<>));

        // Infrastructure: Scoped Dictionary Cache
        services.TryAddScoped(typeof(ScopedDictionaryCache<>));

        // Infrastructure: Page to MagicPage conversion
        services.TryAddTransient<IMagicPageService, MagicPageService>();

        return services;
    }

    public static IServiceCollection AddCre8magicSettingsCore(this IServiceCollection services)
    {
        // All these Settings etc. should be scoped, so they don't have to reload for each click
        services.TryAddScoped<IMagicSettingsService, MagicSettingsService>();

        // Services used by SettingsService, which is scoped, so the dependencies can be normal transient
        services.TryAddTransient<MagicSettingsCatalogsLoader>();

        return services;
    }

    public static IServiceCollection AddCre8magicSettingsSourcesPackage(this IServiceCollection services)
    {
        // 2024-11-21 2dm test, was registered both scoped and transient, probably transient is ok
        //services.TryAddScoped<MagicSettingsCatalogLoaderJson>();
        services.TryAddTransient<MagicSettingsCatalogLoaderJson>();
        services.AddScoped<IMagicSettingsSource, MagicSettingsSourceJson>();
        services.AddTransient<IMagicSettingsSource, MagicSettingsSourcePackageDefaults>();

        return services;
    }
    public static IServiceCollection AddCre8magicSettingsSourceProvider(this IServiceCollection services)
    {
        // Main Settings Provider, scoped, to be used on two following interfaces
        services.TryAddScoped<MagicSettingsProvider>();
        services.TryAddTransient<IMagicSettingsProvider>(s => s.GetRequiredService<MagicSettingsProvider>());
        services.AddTransient<IMagicSettingsSource>(s => s.GetRequiredService<MagicSettingsProvider>());

        return services;
    }



    public static IServiceCollection AddCre8magicMainParts(this IServiceCollection services)
    {
        services.TryAddTransient<IMagicLanguageService, MagicLanguageService>();
        services.TryAddTransient<IMagicThemeService, MagicThemeService>();
        services.TryAddTransient<IMagicPageContextService, MagicPageContextService>();
        services.TryAddTransient<IMagicUserService, MagicUserService>();
        services.TryAddTransient<IMagicAnalyticsService, MagicAnalyticsService>();
        services.TryAddTransient<IMagicBreadcrumbService, MagicBreadcrumbService>();
        services.TryAddTransient<IMagicMenuService, MagicMenuService>();
        services.TryAddTransient<IUserLoginService, UserLoginService>();
        services.TryAddTransient<IMagicLinkService, MagicLinkService>();
        services.TryAddTransient<IMagicContainerService, MagicContainerService>();

        services.TryAddTransient<IMagicHat, MagicHat>();

        // Logic parts for Controls - TODO: naming / purpose etc. needs to be adjusted
        services.TryAddTransient<MagicPageEditService>();

        return services;
    }

    public static IServiceCollection AddCre8magicJsLayer(this IServiceCollection services)
    {
        services.TryAddTransient<MagicThemeJsServiceTest>();

        return services;
    }

    public static IServiceCollection AddOqtaneWorkarounds(this IServiceCollection services)
    {
        services.TryAddTransient<OqtaneLoginHelperWip>();

        return services;
    }

}