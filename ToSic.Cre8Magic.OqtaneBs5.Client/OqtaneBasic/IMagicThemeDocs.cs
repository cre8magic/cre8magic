using Oqtane.Documentation;
using ToSic.Cre8magic.Act;
using ToSic.Cre8magic.Themes;

namespace ToSic.Cre8magic.OqtaneBasic;

[PrivateApi]
internal interface IMagicThemeDocs
{
    /// <summary>
    /// This contains the default settings which must be used in this theme.
    /// Any inheriting class must specify what it will be. 
    /// </summary>
    public MagicThemePackage ThemePackage { get; }

    /// <summary>
    /// The Theme Kit which is help the theme become awesome.
    /// </summary>
    public IMagicThemeKit ThemeKit { get; }

    /// <summary>
    /// The <see cref="MagicAct"/> which coordinates everything.
    /// </summary>
    /// <remarks>
    /// It's provided by Dependency Injection, required.
    /// </remarks>
    public IMagicAct MagicAct { get; }

    /// <summary>
    /// Handle OnParametersSet to provide the latest PageState to the MagicAct.
    /// </summary>
    /// <remarks>
    /// OnParametersSet runs whenever any parameter changes - such as PageState.
    /// It also runs before OnParametersSetAsync.
    /// </remarks>
    protected void OnParametersSet();


    /// <summary>
    /// Handle OnAfterRenderAsync to track page views
    /// </summary>
    protected Task OnAfterRenderAsync(bool isFirstRender);


}