using System.Threading;
using Selenium.Entities;
using Xunit;

namespace Selenium.Tests;

// public class 

[Collection("Collection")]
public class TaskTests
{
    private readonly TestsFixture _fixture;

    public TaskTests(TestsFixture fixture) =>
        _fixture = fixture;

    [Theory]
    [InlineData("Task12", "Task12 Text")]
    [InlineData("Task2", "Task2 Text")]
    [InlineData("Task3", "Task3 Text")]
    [InlineData("Task4", "Task4 Text")]
    public void Add_LoggedIn_ShouldCreateNewTask(string title, string text)
    {
        // arrange
        var task = new Task
        {
            Title = title,
            Text = text
        };

        // act
        _fixture.App.AccountHelper.Login(_fixture.Account);

        _fixture.App.TaskHelper.AddTask(task);
        Thread.Sleep(3000);

        // assert
        Assert.Equal(task.Text, _fixture.App.TaskHelper.GetLastCreatedTask().Text);
        Assert.Equal(task.Title, _fixture.App.TaskHelper.GetLastCreatedTask().Title);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void Delete_LoggedInAndTaskExists_ShouldDeleteTask(int taskNumber)
    {
        // act
        _fixture.App.AccountHelper.Login(_fixture.Account);

        var realId = _fixture.App.TaskHelper.GetRealTaskId(taskNumber);
        _fixture.App.TaskHelper.DeleteTask(taskNumber);
        Thread.Sleep(3000);

        // assert
        Assert.False(_fixture.App.TaskHelper.IsTaskExists(realId));
    }
}