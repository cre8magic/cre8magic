namespace ToSic.Cre8magic.Tokens;

internal interface ITokenReplace
{
    string NameId { get; }
    string? Parse(string? value);
}