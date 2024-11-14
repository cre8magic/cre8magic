using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Client.Controls;

public interface IMagicControlWithSettings: IMagicDesigner
{
    [CascadingParameter] MagicAllSettings AllSettings { get; set; }

    //public string? Classes(string target);

    //public string? Value(string target);

    //public string? Id(string target);
}