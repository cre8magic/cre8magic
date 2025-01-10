using Oqtane.Models;
using Oqtane.Shared;
using static Oqtane.Shared.ResourceType;

// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.TestTheme.Client;

/// <summary>
/// Base class for themes which fully leverage cre8magic.
/// </summary>
/// <remarks>
/// It is responsible for
///
/// 1. Some basic properties such as Name, BodyClasses etc. which each theme can configure
/// 2. Adding special classes to the body tag so that the CSS can best optimize for each scenario
/// 
/// Note: This is abstract and has some abstract properties, so you don't forget to set them when you inherit from it. 
/// </remarks>
public abstract class MyThemeBase : ToSic.Cre8magic.OqtaneBs5.MagicTheme
{
    /// <summary>
    /// Standard Oqtane Resources
    /// </summary>
    public override List<Resource> Resources => ThemePackage == null
        ? null
        :
        [
            new() { ResourceType = Stylesheet, Url = $"{ThemePackage.ThemePath}/theme.min.css" }, // Bootstrap generated with Sass/Webpack
            new() { ResourceType = Script, Url = $"{ThemePackage.ThemePath}/bootstrap.bundle.min.js" }, // Bootstrap JS

            // TODO: cre8magic JS/RESOURCES should be supplied by composition
            new() { ResourceType = Script, Url = $"{ThemePackage.ThemePath}/ambient.js", } // Ambient JS for page Up-button etc.
        ];

    /// <summary>
    /// The layout name which is used to select the named settings for this theme.
    /// </summary>
    /// <remarks>
    /// We shouldn't use `Name` for this, as it may change since it's primarily for the user to see.
    /// It's abstract, so the inheriting file is required to set it. 
    /// </remarks>
    public virtual string MagicName { get; }

    /// <summary>
    /// Set the ThemePackage, so the system has all it needs.
    /// </summary>
    public override MagicThemePackage ThemePackage => field ??= new(new ThemeInfo(), MagicName);

    // Panes of the layout
    public const string PaneNameHeader = "Header";

    public override string Panes => string.Join(",", PaneNames.Default, PaneNameHeader);
}