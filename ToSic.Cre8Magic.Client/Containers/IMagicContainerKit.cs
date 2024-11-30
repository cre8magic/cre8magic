namespace ToSic.Cre8magic.Containers;

public interface IMagicContainerKit
{
    MagicContainerTailor Tailor { get; init; }

    /// <summary>
    /// Modules are treated as admin modules (and must use the admin container) if they are marked as such, or come from the Oqtane ....Admin... type
    /// </summary>
    /// <returns></returns>
    bool IsForAdminModule { get; }
}