// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Oqtane.Infrastructure.Interfaces;
using Oqtane.Models;
using ToSic.Util.OqtaneTheme2TemplateApp;
using static ToSic.Util.OqtaneTheme2TemplateApp.ThemeTemplateService;
using File = System.IO.File;

var executingAssembly = Assembly.GetExecutingAssembly();
Console.WriteLine(executingAssembly.FullName);

// Get the command line arguments
var themePath = args.Length > 0
    ? args[0]
    : @"A:\2shine-oqtane\cre8magic\ToSic.Theme.Cre8magic3\";

Console.WriteLine($"Source Path: {themePath}");

// Get the project directory from the AssemblyMetadataAttribute
var projectDirectory = GetProjectDirectory();
var themeJsonFilePath = Path.Combine(projectDirectory, "theme.json");
var theme = ReadOrCreateTheme(themeJsonFilePath);

// get new instance of ThemTemplateService using DI
var themeTemplateService = GetThemeTemplateService();

var templatesPath = Path.Combine(projectDirectory, "Templates");
themeTemplateService.ConvertToTemplate(theme, themePath, templatesPath);

Console.WriteLine("\nPress any key to exit.");
Console.ReadKey();

return;

// Get the project directory from the AssemblyMetadataAttribute
static string? GetProjectDirectory()
{
    // Get the currently executing assembly
    var assembly = Assembly.GetExecutingAssembly();

    // Find the AssemblyMetadataAttribute with the key "ProjectDirectory"
    var metadataAttribute = assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
        .FirstOrDefault(attr => attr.Key == "ProjectDirectory");

    var projectDirectory = metadataAttribute?.Value;
    if (projectDirectory != null)
    {
        Console.WriteLine($"Project Directory (from Metadata): {projectDirectory}");
        return projectDirectory;
    }

    //// otherwise return current executing directory
    //if (projectDirectory != null)
    //{
    //    projectDirectory = Path.GetDirectoryName(assembly.Location);
    //    Console.WriteLine($"Project Directory (from Executing Assembly): {projectDirectory}");
    //    return projectDirectory;
    //}

    // Get the current working directory
    projectDirectory = Directory.GetCurrentDirectory();
    Console.WriteLine($"Project Directory (from Current Directory): {projectDirectory}");
    return projectDirectory;
}

// Read or create the theme.json file
Theme ReadOrCreateTheme(string themeSettingsJsonPath)
{
    // default theme settings
    Theme theme = new()
    {
        Owner = "ToSic",
        Name = "Cre8magic3",
        ThemeName = "ToSic.Theme.Cre8magic3",
        Template = "Cre8magic",
        Version = "6.1.1",
        PackageName = "ToSic.Theme.Cre8magic3",
        ThemeSettingsType = "ToSic.Theme.Cre8magic3.ThemeSettings, ToSic.Theme.Cre8magic3.Client.Oqtane",
        ContainerSettingsType = "ToSic.Theme.Cre8magic3.ContainerSettings, ToSic.Theme.Cre8magic3.Client.Oqtane",
    };

    JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
        IgnoreReadOnlyFields = true,
        IgnoreReadOnlyProperties = true,
        AllowTrailingCommas = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver() // Configure TypeInfoResolver
    };

    // check file not exist
    if (File.Exists(themeSettingsJsonPath))
    {
        // read file, deserialize to Theme object
        var existingThemeJson = File.ReadAllText(themeSettingsJsonPath);
        var existingTheme = JsonSerializer.Deserialize<Theme>(existingThemeJson, options);
        if (existingTheme != null)
        {
            theme = existingTheme;
            Console.WriteLine($"Existing Theme: {theme}");
        }
        else
        {
            Console.WriteLine("Failed to deserialize existing theme JSON.");
        }
    }
    else
    {
        Console.WriteLine($"Theme JSON file does not exist: {themeSettingsJsonPath}");
        var themeJson = JsonSerializer.Serialize(theme, options: options);

        // write to file
        File.WriteAllText(themeSettingsJsonPath, themeJson);
        Console.WriteLine($"Theme JSON saved to: {themeSettingsJsonPath}");
    }

    return theme;
}

ThemeTemplateService GetThemeTemplateService()
{
    var serviceProvider = new ServiceCollection()
        .AddLogging()
        .AddTransient<ILogManager, UtilLogManager>()
        .AddTransient<ITokenReplace, TokenReplaceReverse>()
        .AddTransient<ThemeTemplateService>()
        .BuildServiceProvider();

    var themeTemplateService1 = serviceProvider.GetRequiredService<ThemeTemplateService>();
    return themeTemplateService1;
}
