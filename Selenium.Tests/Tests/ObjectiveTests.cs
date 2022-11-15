using System.Threading;
using Selenium.Models;
using Xunit;

namespace Selenium.Tests.Tests;

[Collection("Collection")]
public class ObjectiveTests
{
    private readonly TestsFixture _fixture;

    public ObjectiveTests(TestsFixture fixture) =>
        _fixture = fixture;

    [Theory]
    [MemberData(nameof(TestsFixture<Objective>.GetData), parameters: 3, MemberType = typeof(TestsFixture<Objective>))]
    public void Add_LoggedIn_ShouldCreateNewObjective(string title, string text)
    {
        // arrange
        var objective = new Objective { Title = title, Text = text };

        // act
        _fixture.App.AccountHelper.Login(_fixture.Account);

        _fixture.App.ObjectiveHelper.AddObjective(objective);
        Thread.Sleep(3000);

        // assert
        Assert.Equal(objective.Text, _fixture.App.ObjectiveHelper.GetLastCreatedObjective().Text);
        Assert.Equal(objective.Title, _fixture.App.ObjectiveHelper.GetLastCreatedObjective().Title);
    }

    [Theory]
    [InlineData(1)]
    public void Delete_LoggedInAndObjectiveExists_ShouldDeleteObjective(int objectNumber)
    {
        // act
        _fixture.App.AccountHelper.Login(_fixture.Account);

        var realId = _fixture.App.ObjectiveHelper.GetRealObjectiveId(objectNumber);
        _fixture.App.ObjectiveHelper.DeleteObjective(objectNumber);
        Thread.Sleep(3000);

        // assert
        Assert.False(_fixture.App.ObjectiveHelper.IsObjectiveExists(realId));
    }
}