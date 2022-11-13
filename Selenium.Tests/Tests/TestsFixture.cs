using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Selenium.Models;
using Xunit;

namespace Selenium.Tests.Tests;

public class TestsFixture : TestBase, IDisposable
{
    public ApplicationManager App { get; }
    public Account Account { get; }

    public TestsFixture()
    {
        Account = new Account { Email = BaseEmail, Password = BasePassword };
        App = ApplicationManager.GetInstance();
    }

    public void Dispose() =>
        App.Dispose();
}

public class TestsFixture<T>
{
    public static List<T> GetEntityFromXmlFile()
    {
        var xRoot = new XmlRootAttribute { ElementName = $"{typeof(T).Name}s", IsNullable = true };
        var reader = new StreamReader(
            $@"{ProjectConfiguration.ProjectRootDirectory}\{ProjectConfiguration.OutputDirectoryPath}" +
            $@"\{ProjectConfiguration.NormalizeFileName(typeof(T).Name.ToLower())}");

        return (List<T>)new XmlSerializer(typeof(List<T>), xRoot).Deserialize(reader)!;
    }
}

[CollectionDefinition("Collection")]
public class TestsFixtureCollection : ICollectionFixture<TestsFixture>
{
}