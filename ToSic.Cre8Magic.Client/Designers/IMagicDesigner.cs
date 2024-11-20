namespace ToSic.Cre8magic.Designers;

public interface IMagicDesigner
{
    public string? Classes(string target);

    public string? Value(string target);

    public string? Id(string target);
}