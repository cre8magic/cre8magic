using Microsoft.AspNetCore.Components;
using Oqtane.UI;
using ToSic.Cre8magic.Themes.Internal;

namespace ToSic.Cre8magic.Components;

/// <summary>
/// Non-Oqtane Blazor component with Settings as base for your controls
/// </summary>
public abstract class MagicComponentBase: ComponentBase
{
    [CascadingParameter]
    protected PageState PageState { get; set; }

    [Inject]
    public IMagicFactoryWip MagicFactory { get; set; }

    private MagicThemeDesigner Designer => _designer ??= MagicFactory.ThemeDesigner(PageState);
    private MagicThemeDesigner? _designer;

    public string? Classes(string target) => Designer.Classes(target);

    public string? Value(string target) => Designer.Value(target);

    public string? Id(string name) => Designer.Id(name);
}