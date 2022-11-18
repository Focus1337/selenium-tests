using TestDataXmlGenerator;

Console.WriteLine("Choose Data Type for data generation.\n1 - Account | 2 - Project | 3 - Objective: ");
var dataType = DataGenerator.GetDataType(Console.ReadLine() ?? "0");

Console.Write("Give name for your test data. (Available: exist, not_exist, add, update): "); // мне лень делать проверки на это
var testDataName = Console.ReadLine() ?? string.Empty;

Console.Write("\nHow many objects you want to generate?\nMinimum 1 object. Provide integer number: ");
if (!int.TryParse(Console.ReadLine(), out var count))
    throw new ArgumentException("You had to provide integer number!\nRestart and try again.");
if (count <= 0)
    throw new ArgumentException("You had to write at least one integer number.");

var generator = new DataGenerator(dataType, testDataName, count);
var data = generator.GenerateData();

Console.WriteLine($"\n\n\nYour result:\n {data}\n\n\n");
generator.SaveFile(data);