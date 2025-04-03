using Oqtane.Infrastructure.Interfaces;
using Oqtane.Models;
using Oqtane.Shared;

namespace ToSic.Util.OqtaneTheme2TemplateApp;

public partial class ThemeTemplateService(ITokenReplace tokenReplace)
{
    private static readonly List<string> SkipFolders = ["bin", "obj"];

    public bool ConvertToTemplate(Theme theme, string themePath, string templatesPath)
    {

        Console.WriteLine($"ThemePath: {themePath}");
        if (!Directory.Exists(themePath))
            throw new DirectoryNotFoundException($"Theme path does not exist: {themePath}");

        var templatePath = Utilities.PathCombine(templatesPath, theme.Template, Path.DirectorySeparatorChar.ToString());
        Console.WriteLine($"TemplatePath: {templatePath}");
        if (Directory.Exists(templatePath))
            Directory.Delete(templatePath, true);
        Directory.CreateDirectory(templatePath);

        var rootFolder = "[Owner].Theme.[Theme]";

        theme.ThemeName = !string.IsNullOrEmpty(theme.ThemeName)
            ? theme.ThemeName.Replace(theme.Owner, "[Owner]").Replace(theme.Name, "[Theme]")
            : rootFolder;

        theme.ThemeName = theme.ThemeName + ", " + theme.ThemeName + ".Client.Oqtane";
        Console.WriteLine($"ThemeName: {theme.ThemeName}");

        ProcessTemplatesRecursively(theme, themePath, new DirectoryInfo(themePath), templatePath);

        return true;
    }

    private void ProcessTemplatesRecursively(Theme theme, string themeRootPath, DirectoryInfo themeCurrentDirectoryInfo, string templatePath)
    {
        var tokenReplace = InitializeTokenReplace(theme);

        // process folder
        var themeSegment = themeCurrentDirectoryInfo.FullName.Replace(themeRootPath, "");
        var templateSegment = tokenReplace.ReplaceTokens(themeSegment);
        var templateFolderPath = Utilities.PathCombine(templatePath, templateSegment);
        if (!Directory.Exists(templateFolderPath)) Directory.CreateDirectory(templateFolderPath);

        // tokenReplace.AddSource("Folder", folderPath);
        var themeFiles = themeCurrentDirectoryInfo.GetFiles("*.*");
        if (themeFiles == null) return;

        foreach (var themeFile in themeFiles)
        {
            // process file
            var templateFileName = tokenReplace.ReplaceTokens(themeFile.Name);
            var templateFilePath = Path.Combine(templateFolderPath, templateFileName);
            // tokenReplace.AddSource("File", Path.GetFileName(templateFilePath));

            var themeFileContent = System.IO.File.ReadAllText(themeFile.FullName);
            var templateFileContent = tokenReplace.ReplaceTokens(themeFileContent);
            System.IO.File.WriteAllText(templateFilePath, templateFileContent);
        }

        var themeFolders = themeCurrentDirectoryInfo.GetDirectories()
            .Where(f => !Enumerable.Contains(SkipFolders, f.Name))
            .ToArray();

        foreach (var themeCurrentFolder in themeFolders)
            ProcessTemplatesRecursively(theme, themeRootPath, themeCurrentFolder, templatePath);
    }

    private ITokenReplace InitializeTokenReplace(Theme theme)
    {
        tokenReplace.AddSource(() =>
        {
            return new Dictionary<string, object>
            {
                // { "RootPath", themeRootPath },
                // { "RootFolder", theme.Template },
                { "Owner", theme.Owner },
                { "Theme", theme.Name  },
                { "FrameworkVersion", theme.Version },
                // { "ClientReference", $"<Reference Include=\"Oqtane.Client\"><HintPath>..\\..\\{rootFolder}\\Oqtane.Server\\bin\\Debug\\net9.0\\Oqtane.Client.dll</HintPath></Reference>" },
                // { "SharedReference", $"<Reference Include=\"Oqtane.Shared\"><HintPath>..\\..\\{rootFolder}\\Oqtane.Server\\bin\\Debug\\net9.0\\Oqtane.Shared.dll</HintPath></Reference>" },
                { "ClientReference", $"<PackageReference Include=\"Oqtane.Client\" Version=\"{theme.Version}\" />" },
                { "SharedReference", $"<PackageReference Include=\"Oqtane.Shared\" Version=\"{theme.Version}\" />" },
                // { templateRootPath, "[RootPath]" },
                // { templateRootFolder, "[RootFolder]" },
            };
        });

        return tokenReplace;
    }
}