namespace Selenium;

public static class ProjectConfiguration
{
    public static DirectoryInfo? ProjectRootDirectory =>
        new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent!.Parent!.Parent!.Parent;

    public static string NormalizeFileName(string fileName) =>
        $"{fileName}s_data_generated.xml";

    public const string OutputDirectoryPath = "GeneratedData";
}