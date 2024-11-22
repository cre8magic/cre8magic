using Oqtane.Models;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers;

internal record MagicContainerKit : IMagicContainerKit
{
    public required MagicContainerDesigner Designer { get; init; }

    internal required Module Module { get; init; }

    public bool IsForAdminModule => Module.ForceAdminContainer();
}