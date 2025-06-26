﻿using Oqtane.UI;

namespace ToSic.Cre8magic.PageContexts.Internal;

public interface IMagicPageContextService
{
    IMagicPageContextKit PageContextKit(PageState pageState, MagicPageContextSettings? settings = default);
}