using System.Xml.Linq;
using Selenium.Models;

namespace TestDataXmlGenerator.Generators;

internal class DataGenerator
{
    private XElement Entities { get; set; }
    private int Count { get; set; }
    private string EntityName { get; set; }

    public DataGenerator(string entityName, int elementsCount)
    {
        EntityName = entityName;
        Entities = new XElement($"{entityName}s");
        Count = elementsCount;
    }

    public XElement GenerateEntities()
    {
        for (var i = 0; i < Count; i++)
        {
            Console.Write($"Write Title for Project {i}: ");
            Entities.Add(new XElement(EntityName, new XElement("Title", Console.ReadLine())));
        }

        return Entities;
    }

    public void SaveFile(XElement entities)
    {
        var fileName = $"{EntityName.ToLower()}s_data_generated.xml";
        var directory = $@"{Directory.GetCurrentDirectory()}\{fileName}";
        Console.WriteLine($"Saved into: {directory}\n");

        entities.Save(directory);
    }

    public static string GetEntityName(string? consoleInput) =>
        Convert.ToInt32(consoleInput) switch
        {
            1 => nameof(Account),
            2 => nameof(Project),
            3 => nameof(Objective),
            _ => throw new ArgumentOutOfRangeException(nameof(consoleInput),
                "Provided number is outside of allowed range")
        } ?? throw new NullReferenceException();
}