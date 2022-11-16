using System.Reflection;
using System.Xml.Linq;
using Selenium;
using Selenium.Models;

namespace TestDataXmlGenerator;

internal class DataGenerator
{
    private XElement Entities { get; }
    private Type EntityType { get; }
    private int Count { get; }
    private string EntityName { get; }
    private PropertyInfo[]? _properties;
    private PropertyInfo[] Properties => _properties ??= EntityType.GetProperties();

    public DataGenerator(Type entityType, int elementsCount)
    {
        EntityType = entityType;
        EntityName = entityType.Name;
        Count = elementsCount;
        Entities = new XElement($"{EntityName}s");
    }

    public XElement GenerateEntities()
    {
        for (var i = 0; i < Count; i++)
        {
            Entities.Add(new XElement(EntityName));

            foreach (var property in Properties)
            {
                var propertyName = property.Name;
                Console.Write($"Write {propertyName} for {EntityType.Name} {i}: ");
                Entities.Elements().Last().Add(new XElement(propertyName, Console.ReadLine()));
            }
        }

        return Entities;
    }

    public void SaveFile(XElement entities)
    {
        var directoryPath = $@"{ProjectConfiguration.ProjectRootDirectory}\{ProjectConfiguration.OutputDirectoryPath}";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var directory = $@"{directoryPath}\{ProjectConfiguration.NormalizeFileName(EntityName.ToLower())}";

        Console.WriteLine($"Saved into: {directory}\n");

        entities.Save(directory);
    }

    public static Type GetEntityType(string consoleInput) =>
        Convert.ToInt32(consoleInput) switch
        {
            1 => typeof(Account),
            2 => typeof(Project),
            3 => typeof(Objective),
            _ => throw new ArgumentOutOfRangeException(nameof(consoleInput),
                "Provided number is outside of allowed range")
        };
}