using System.Xml.Linq;

var projects =
    new XElement("Projects");

Console.Write("How many entities you want to generate? Provide at least one integer number: ");
if (!int.TryParse(Console.ReadLine(), out var count))
    throw new ArgumentException("You had to provide integer number, dumbass!\nRestart and try again.");

if (count <= 0)
    throw new ArgumentException("You had to write at least one integer number.");

for (var i = 0; i < count; i++)
{
    Console.Write($"Write Title for Project {i}: ");
    projects.Add(new XElement("Project", new XAttribute("Id", i.ToString()),
        new XElement("Title", Console.ReadLine())));
}

Console.WriteLine(projects);