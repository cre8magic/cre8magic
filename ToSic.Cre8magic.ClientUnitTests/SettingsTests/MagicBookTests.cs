using ToSic.Cre8magic.Links;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tailors;
using ToSic.Cre8magic.UserLogins;
using ToSic.Cre8magic.Users;
using Xunit.Abstractions;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsTests;

public class MagicBookTests(ITestOutputHelper output)
{
    /// <summary>
    /// These types are not currently implemented in the MagicBook
    /// </summary>
    private readonly List<Type> ExcludedTypes = [
        typeof(MagicUserSettings),
        typeof(MagicUserLoginSettings),
        typeof(MagicBlueprint),
        typeof(MagicLinkSettings),
    ];

    private List<Type> GetAllSettingsTypes()
    {
        // Get the assembly where MagicInheritsBase is defined
        var assembly = typeof(MagicInheritsBase).Assembly;

        // Find all types that inherit from MagicInheritsBase
        return assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(MagicInheritsBase)) && !t.IsAbstract)
            .ToList();
    }

    private List<Type> GetActiveSettingsTypes() =>
        GetAllSettingsTypes()
            .Where(t => !ExcludedTypes.Contains(t))
            .ToList();

    [Fact]
    public void MagicBook_ContainsAllSettings()
    {
        // Arrange
        var settingsTypes = GetActiveSettingsTypes();
        var magicBook = new MagicBook();

        // Act
        var magicBookTypes = magicBook.GetType().GetProperties()
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            .Select(p => p.PropertyType.GetGenericArguments()[1])
            .ToList();

        var missingTypes = new List<Type>();

        // Assert
        foreach (var type in settingsTypes)
        {
            if (!magicBookTypes.Contains(type))
            {
                missingTypes.Add(type);
                output.WriteLine($"Missing type: {type.Name}");
            }
        }

        Assert.Empty(missingTypes);
    }

    [Fact]
    public void MagicBook_GetSection_WorksForAllSettingsTypes()
    {
        // Arrange
        var settingsTypes = GetActiveSettingsTypes();
        var magicBook = new MagicBook();

        var failedTypes = new List<Type>();

        // Act
        foreach (var type in settingsTypes)
        {
            try
            {
                var method = typeof(MagicBook).GetMethod("GetSection")?.MakeGenericMethod(type);
                method?.Invoke(magicBook, null);
            }
            catch (Exception ex)
            {
                failedTypes.Add(type);
                output.WriteLine($"Failed type: {type.Name}, Exception: {ex.Message}");
            }
        }

        // Assert
        Assert.Empty(failedTypes);
    }
}