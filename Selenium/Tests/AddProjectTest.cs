using System.Threading;
using NUnit.Framework;
using Selenium.Entities;

namespace Selenium.Tests;

[TestFixture]
public class AddProjectTest : TestBase
{
    [Test]
    public void LogInAndCreateProject_ShouldCreateNewProject()
    {
        var account = new Account(BaseEmail, BasePassword);
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