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
            Title = "ehtr3rj3tyj"
        };

        ManageWindowSize(1920, 1040);
        Login(account);
        Thread.Sleep(8000);

        AddNewProject(project);
        Thread.Sleep(3000);
    }
}