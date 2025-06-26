using Oqtane.UI;

namespace ToSic.Cre8magic.Utils;

[PrivateApi]
public interface IHasPageState
{
    internal PageState PageState { get; }
}