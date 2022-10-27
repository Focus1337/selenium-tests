using System.Threading;
using NUnit.Framework;
using Selenium.Base;
using Selenium.Entities;

namespace Selenium.Tests;

[TestFixture]
public class AddProjectTest : TestBase
{
    [Test]
    public void AddProject()
    {
        var account = new Account("eddie.1o1@bk.ru", "121233aa");
        var project = new Project
        {
            Title = "semenar 3"
        };

        _app.LoginHelper.Login(account);
        Thread.Sleep(8000);

        _app.ProjectHelper.AddNewProject(project);
        Thread.Sleep(3000);
    }
}