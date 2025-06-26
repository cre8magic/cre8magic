using Oqtane.Models;
using Oqtane.Shared;
using Oqtane.Themes;
using ToSic.Cre8magic.OqtaneBasic;

// Important: This must match the namespace of the layouts
// otherwise the Oqtane registration won't work as expected.
namespace ToSic.Cre8magic.TestTheme.Client;

public class ThemeInfo : ITheme
{
    /// <summary>
    /// The standard information object for Oqtane to register the theme.
    /// </summary>
    public Theme Theme => new()
    {
        Name = "cre8magic Test Theme",
        Version = "0.3.1",

        // Package Name - used for NuGet and also for paths
        PackageName = "ToSic.Cre8magic.TestTheme",
        Dependencies = typeof(MagicTheme).AssemblyQualifiedName,
        Resources =
        [
            new()
            {
                ResourceType = ResourceType.Stylesheet,
                Url = "~/theme.min.css"
            }, // Bootstrap generated with Sass/Webpack
            new()
            {
                ResourceType = ResourceType.Script,
                Url = "~/bootstrap.bundle.min.js"
            }, // Bootstrap JS
            new()
            {
                ResourceType = ResourceType.Script,
                Url = "~/test.js"
            }, // Bootstrap JS
            ..MagicResources.GetAll(),
        ],
    };

}
