namespace ToSic.Cre8magic.Pages;

/// <summary>
/// Simple page designer for breadcrumbs or menus.
/// If no lookups are specified, the Classes(...) and Data(...) methods will return empty strings.
/// </summary>
/// <remarks>
/// This is also a great candidate to inherit from, as you can specify the Lookups in the constructor.
/// The methods are also virtual, so you can override them as needed.
/// </remarks>
public class MagicPageTailorBasic : MagicPageTailorBase
{
    public static string All = "*";

    /// <summary>
    /// Dictionary which is used to lookup what classes to provide for each tag.
    /// Can be specified when creating an instance of this class.
    /// We recommend to supply a case-insensitive dictionary.
    /// </summary>
    public IDictionary<string, string> LookupClasses { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// The class to add to the active item.
    /// Note that it will be applied to all tags.
    /// For more advanced scenarios, you may want to override the Classes method.
    /// </summary>
    public string? LookupClassActive { get; init; }

    /// <summary>
    /// Very basic implementation of the Classes generator.
    /// Will combine classes from the ClassLookup and ClassActive properties.
    /// </summary>
    /// <returns>a space-separated list of classes, or null</returns>
    public override string? Classes(string tag, IMagicPage item)
    {
        // List to store CSS class names
        var classes = new List<string>();

        if (LookupClasses.TryGetValue(All, out var tagClass))
            classes.Add(tagClass);

        if (LookupClasses.TryGetValue(tag, out tagClass))
            classes.Add(tagClass);

        if (item.IsActive && LookupClassActive != null)
            classes.Add(LookupClassActive);

        // Return the CSS classes as a space-separated string
        return classes.Any() ? string.Join(" ", classes) : null;
    }

    /// <summary>
    /// Dictionary which is used to lookup what data/values to provide for each tag.
    /// Can be specified when creating an instance of this class.
    /// We recommend to supply a case-insensitive dictionary.
    /// </summary>
    public IDictionary<string, string> LookupData { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Very basic implementation of the Data/Value generator.
    /// Will use the <see cref="LookupData"/> to find the value for the key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public override string? Value(string key, IMagicPage item)
        => LookupData.TryGetValue(key, out var val) ? val : null;
}