namespace ToSic.Cre8magic.Tokens;

public interface ITokenReplace
{
    string NameId { get; }
    string? Parse(string? value);
}