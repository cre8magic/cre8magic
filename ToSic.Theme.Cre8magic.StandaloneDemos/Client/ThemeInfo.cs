using Oqtane.Themes;
using Oqtane.Shared;

namespace ToSic.Theme.Cre8magic.StandaloneDemos;

public class ThemeInfo : ITheme
{
    public Oqtane.Models.Theme Theme => new()
    {
        Name = "ToSic Cre8magic StandaloneDemos",
        Version = "1.0.0",
        PackageName = "ToSic.Theme.Cre8magic.StandaloneDemos",
        ThemeSettingsType = "ToSic.Theme.Cre8magic.StandaloneDemos.ThemeSettings, ToSic.Theme.Cre8magic.StandaloneDemos.Client.Oqtane",
        ContainerSettingsType = "ToSic.Theme.Cre8magic.StandaloneDemos.ContainerSettings, ToSic.Theme.Cre8magic.StandaloneDemos.Client.Oqtane",
        Resources =
        [
            new()
            {
                ResourceType = ResourceType.Stylesheet,
                Url = "https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.3/css/bootstrap.min.css",
                Integrity = "sha512-jnSuA4Ss2PkkikSOLtYs8BlYIeeIK1h99ty4YfvRPAlzr377vr3CXDb7sb7eEEBYjDtcYj+AjBH3FLv5uSJuXg==",
                CrossOrigin = "anonymous"
            },
            new()
            {
                ResourceType = ResourceType.Stylesheet,
                Url = "~/Theme.css"
            },
            new()
            {
                ResourceType = ResourceType.Script,
                Url = "https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.3/js/bootstrap.bundle.min.js",
                Integrity = "sha512-7Pi/otdlbbCR+LnW+F7PwFcSDJOuUJB3OxtEHbg4vSMvzvJjde4Po1v4BR9Gdc9aXNUNFVUY+SK51wWT8WF0Gg==",
                CrossOrigin = "anonymous"
            }
        ]
    };

}