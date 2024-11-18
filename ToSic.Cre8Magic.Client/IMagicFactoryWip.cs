using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Themes.Internal;

namespace ToSic.Cre8magic;

public interface IMagicFactoryWip
{
    internal MagicContainerDesigner ContainerDesigner(PageState pageState, Module module);
    internal MagicThemeDesigner ThemeDesigner(PageState pageState);

    internal MagicLanguageDesigner LanguageDesigner(PageState pageState);
}