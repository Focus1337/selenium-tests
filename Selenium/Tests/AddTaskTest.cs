using System.Threading;
using NUnit.Framework;
using Selenium.Base;
using Selenium.Entities;

namespace Selenium.Tests;

[TestFixture]
public class AddTaskTest : TestBase
{
    [Test]
    public void Run()
    {
        var account = new Account("eddie.1o1@bk.ru", "121233aa");
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