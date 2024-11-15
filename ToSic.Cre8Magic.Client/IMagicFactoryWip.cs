using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Languages.Settings;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;

namespace ToSic.Cre8magic;

public interface IMagicFactoryWip
{
    internal ContainerDesigner ContainerDesigner(PageState pageState, Module module);
    internal ThemeDesigner ThemeDesigner(PageState pageState);

    internal LanguagesDesigner LanguagesDesigner(PageState pageState);
}