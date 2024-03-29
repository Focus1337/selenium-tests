﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Selenium.Models;
using Xunit;

namespace Selenium.Tests.Tests;

public class TestsFixture : IDisposable
{
    public ApplicationManager App { get; }
    public Account Account { get; }

    public TestsFixture()
    {
        Account = new Account { Email = ProjectConfiguration.Email, Password = ProjectConfiguration.Password };
        App = ApplicationManager.GetInstance();
    }

    public void Dispose() =>
        App.Dispose();
}

public class TestsFixture<T>
{
    private static IEnumerable<T> GetEntityFromXmlFile(string testDataName)
    {
        var xRoot = new XmlRootAttribute { ElementName = $"{typeof(T).Name}s", IsNullable = true };
        var reader = new StreamReader(
            $@"{ProjectConfiguration.ProjectRootDirectory}\{ProjectConfiguration.OutputDirectoryName}" +
            $@"\{ProjectConfiguration.GetNormalizedFileName(testDataName, typeof(T).Name.ToLower())}");

        return (List<T>)new XmlSerializer(typeof(List<T>), xRoot).Deserialize(reader)!;
    }

    public static IEnumerable<object[]> GetData(int numTests, string testDataName)
    {
        foreach (var entity in GetEntityFromXmlFile(testDataName).Take(numTests))
        {
            var properties = entity!.GetType().GetProperties();
            var newObject = new object[properties.Length];

            for (var i = 0; i < newObject.Length; i++)
                newObject[i] = properties[i].GetValue(entity) ?? " ";

            yield return newObject;
        }
    }
}

[CollectionDefinition("Collection")]
public class TestsFixtureCollection : ICollectionFixture<TestsFixture>
{
}