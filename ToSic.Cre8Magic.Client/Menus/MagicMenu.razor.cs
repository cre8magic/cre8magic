using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// Base class for any menu list
/// </summary>
public abstract class MagicMenu : MagicMenuBase
{
#pragma warning disable CS8618
    [Parameter, EditorRequired] public IMagicPageList Menu { get; set; }
#pragma warning restore CS8618

    //public string GetUrl(MagicMenuPage page) => GetUrl(page.OriginalPage);

}