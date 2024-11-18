using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Client.Controls;

public interface IMagicControlWithSettings //: IMagicDesigner
{
    [CascadingParameter] MagicAllSettings AllSettings { get; set; }

}