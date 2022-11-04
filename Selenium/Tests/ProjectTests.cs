using System.Threading;
using Selenium.Entities;
using Xunit;

namespace Selenium.Tests;

[Collection("Collection")]
public class ProjectTests
{
    private readonly TestsFixture _fixture;

    public ProjectTests(TestsFixture fixture) => 
        _fixture = fixture;

    // [Fact]
    [Theory]
    [InlineData("ТестИмя1", "ТестИмя1")]
    public void Add_LoggedInAndTitleProvided_ShouldCreateNewProject(string title, string expected)
    {
        // arrage
        // var account = new Account(BaseEmail, BasePassword);
        var project = new Project {Title = title};

        // act
        _fixture.App.LoginHelper.Login(_fixture.Account);
        Thread.Sleep(8000);

        _fixture.App.ProjectHelper.AddNewProject(project);
        Thread.Sleep(3000);

        // assert
        // Assert.Equal(expected, _fixture.App.ProjectHelper.GetLastCreatedProject().Title);
    }
}