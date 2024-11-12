using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Client.Settings;

public interface IHasMagicSettings
{
    MagicAllSettings AllSettings { get; set; }
}