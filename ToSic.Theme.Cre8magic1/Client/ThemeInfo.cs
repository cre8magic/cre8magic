using Oqtane.Models;
using Oqtane.Shared;
using Oqtane.Themes;
using ToSic.Cre8magic.Themes;

namespace ToSic.Theme.Cre8magic1
{
    public class ThemeInfo : ITheme
    {
        public Oqtane.Models.Theme Theme => new Oqtane.Models.Theme
        {
            Name = "ToSic Cre8magic1",
            Version = "1.0.0",
            PackageName = "ToSic.Theme.Cre8magic1",
            ThemeSettingsType = "ToSic.Theme.Cre8magic1.ThemeSettings, ToSic.Theme.Cre8magic1.Client.Oqtane",
            ContainerSettingsType = "ToSic.Theme.Cre8magic1.ContainerSettings, ToSic.Theme.Cre8magic1.Client.Oqtane",
            Resources = [
		        // obtained from https://cdnjs.com/libraries
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.3/css/bootstrap.min.css", Integrity = "sha512-jnSuA4Ss2PkkikSOLtYs8BlYIeeIK1h99ty4YfvRPAlzr377vr3CXDb7sb7eEEBYjDtcYj+AjBH3FLv5uSJuXg==", CrossOrigin = "anonymous" },
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "~/Theme.css" },
                new Resource { ResourceType = ResourceType.Script, Url = "https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.3/js/bootstrap.bundle.min.js", Integrity = "sha512-7Pi/otdlbbCR+LnW+F7PwFcSDJOuUJB3OxtEHbg4vSMvzvJjde4Po1v4BR9Gdc9aXNUNFVUY+SK51wWT8WF0Gg==", CrossOrigin = "anonymous" },
                ..MagicResources.GetAll(),
            ]
        };
    }
}
