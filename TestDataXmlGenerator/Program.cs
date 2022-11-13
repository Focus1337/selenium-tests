using TestDataXmlGenerator;

Console.WriteLine("Choose entity for data generation.\n1 - Account | 2 - Project | 3 - Objective: ");
var entityType = DataGenerator.GetEntityType(Console.ReadLine() ?? "0");

Console.Write("\nHow many objects you want to generate?\nMinimum 1 object. Provide integer number: ");
if (!int.TryParse(Console.ReadLine(), out var count))
    throw new ArgumentException("You had to provide integer number!\nRestart and try again.");
if (count <= 0)
    throw new ArgumentException("You had to write at least one integer number.");

var generator = new DataGenerator(entityType, count);
var entities = generator.GenerateEntities();

Console.WriteLine($"\n\n\nYour result:\n {entities}\n\n\n");
generator.SaveFile(entities);