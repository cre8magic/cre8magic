namespace ToSic.Cre8magic.Tailors;

public interface IMagicTailor
{
    public string? Classes(string target);

    public string? Value(string target);

    public string? Id(string target);
}