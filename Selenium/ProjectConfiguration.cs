using Microsoft.Extensions.Configuration;

namespace Selenium;

public static class ProjectConfiguration
{
    public static string Email { get; }
    public static string Password { get; }
    public static string OutputDirectoryName { get; }

    public static DirectoryInfo? ProjectRootDirectory =>
        new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent!.Parent!.Parent!.Parent;

    public static string GetNormalizedFileName(string testDataName, string fileName) =>
        $"{testDataName}_{fileName}s_data_generated.xml";

    static ProjectConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile($@"{ProjectRootDirectory}\Selenium\appsettings.json", optional: false);

        var configuration = builder.Build();

        Email = configuration["AccountCredentials:Email"];
        Password = configuration["AccountCredentials:Password"];
        OutputDirectoryName = configuration["DataGeneration:OutputDirectoryName"];
    }
}