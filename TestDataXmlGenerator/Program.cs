using TestDataXmlGenerator.Generators;

Console.WriteLine("Choose entity for data generation.\n1 - Account | 2 - Project | 3 - Objective: ");
var entityName = DataGenerator.GetEntityName(Console.ReadLine());

var entities = DataGenerator.GenerateEntities(entityName);

Console.WriteLine($"\n\n\nYour result:\n {entities}\n\n\n");

DataGenerator.SaveFile(entityName, entities);