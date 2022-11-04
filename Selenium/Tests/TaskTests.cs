using System.Threading;
using Selenium.Entities;
using Xunit;

namespace Selenium.Tests;

[Collection("Collection")]
public class TaskTests
{
    private readonly TestsFixture _fixture;

    public TaskTests(TestsFixture fixture) =>
        _fixture = fixture;

    [Fact]
    public void LogInAndEnterTask_ShouldCreateNewTask()
    {
        var task = new Task
        {
            Title = "My New Test",
            Text = "Text text text 123"
        };

        _fixture.App.AccountHelper.Login(_fixture.Account);
        Thread.Sleep(15000);

        _fixture.App.TaskHelper.AddNewTask(task);
        Thread.Sleep(3000);
    }
}