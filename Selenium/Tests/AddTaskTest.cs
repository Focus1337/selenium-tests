using System.Threading;
using NUnit.Framework;
using Selenium.Entities;

namespace Selenium.Tests;

[TestFixture]
public class AddTaskTest : TestBase
{
    [Test]
    public void LogInAndEnterTask_ShouldCreateNewTask()
    {
        var account = new Account(BaseEmail, BasePassword);
        var task = new Task
        {
            Title = "My New Test",
            Text = "Text text text 123"
        };

        _app.LoginHelper.Login(account);
        Thread.Sleep(15000);

        _app.TaskHelper.AddNewTask(task);
        Thread.Sleep(3000);
    }
}