using System.Xml.Linq;
using Selenium.Models;

namespace TestDataXmlGenerator.Generators;

class DataGenerator
{
    public XElement Entity { get; set; }
    
    public DataGenerator(string entityName) => 
        Entity = new XElement($"{entityName}s");

    public static XElement GenerateEntities(string entityName)
    {
        var entities = new XElement($"{entityName}s");

        Console.Write("\nHow many objects you want to generate?\nMinimum 1 object. Provide integer number: ");

        if (!int.TryParse(Console.ReadLine(), out var count))
            throw new ArgumentException("You had to provide integer number, dumbass!\nRestart and try again.");
        if (count <= 0)
            throw new ArgumentException("You had to write at least one integer number.");

        for (var i = 0; i < count; i++)
        {
            Console.Write($"Write Title for Project {i}: ");
            entities.Add(new XElement(entityName, new XAttribute("Id", i.ToString()),
                new XElement("Title", Console.ReadLine())));
        }

        return entities;
    }

    public static void SaveFile(string entityName, XElement entities)
    {
        var fileName = $"{entityName.ToLower()}s_data_generated.xml";
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