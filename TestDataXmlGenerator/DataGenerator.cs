using System.Reflection;
using System.Xml.Linq;
using Selenium;
using Selenium.Models;

namespace TestDataXmlGenerator;

internal class DataGenerator
{
    private XElement Data { get; }
    private Type DataType { get; }
    private int Count { get; }
    private string DataName { get; }
    private string TestDataName { get; }
    private PropertyInfo[]? _properties;
    private PropertyInfo[] Properties => _properties ??= DataType.GetProperties();

    public DataGenerator(Type dataType, string testDataName, int elementsCount)
    {
        DataType = dataType;
        DataName = dataType.Name;
        Count = elementsCount;
        TestDataName = testDataName;
        Data = new XElement($"{DataName}s");
    }

    public XElement GenerateData()
    {
        for (var i = 0; i < Count; i++)
        {
            Data.Add(new XElement(DataName));

            foreach (var property in Properties)
            {
                var propertyName = property.Name;
                Console.Write($"Write {propertyName} for {DataType.Name} {i}: ");
                Data.Elements().Last().Add(new XElement(propertyName, Console.ReadLine()));
            }
        }

        return Data;
    }

    public void SaveFile(XElement entities)
    {
        var directoryPath = $@"{ProjectConfiguration.ProjectRootDirectory}\{ProjectConfiguration.OutputDirectoryName}";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var directory =
            $@"{directoryPath}\{ProjectConfiguration.GetNormalizedFileName(TestDataName, DataName.ToLower())}";

        Console.WriteLine($"Saved into: {directory}\n");

        entities.Save(directory);
    }

    public static Type GetDataType(string consoleInput) =>
        Convert.ToInt32(consoleInput) switch
        {
            1 => typeof(Account),
            2 => typeof(Project),
            3 => typeof(Objective),
            _ => throw new ArgumentOutOfRangeException(nameof(consoleInput),
                "Provided number is outside of allowed range")
        };
}