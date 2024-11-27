using Oqtane.Models;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers.Internal;

internal record MagicContainerKit : IMagicContainerKit
{
    public required MagicContainerSettings Settings { get; init; }

    public required MagicContainerDesigner Designer { get; init; }

    internal required Module Module { get; init; }

    public bool IsForAdminModule => Module.ForceAdminContainer();
}