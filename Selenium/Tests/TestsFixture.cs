using System;
using Selenium.Entities;
using Xunit;

namespace Selenium.Tests;

public class TestsFixture : TestBase, IDisposable
{
    public ApplicationManager App { get; }
    public Account Account { get; }

    public TestsFixture()
    {
        Account = new Account(BaseEmail, BasePassword);
        App = ApplicationManager.GetInstance();
    }

    public void Dispose() => 
        App.Dispose();
}

[CollectionDefinition("Collection")]
public class TestsFixtureCollection : ICollectionFixture<TestsFixture>
{
}