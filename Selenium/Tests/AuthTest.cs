using System.Threading;
using NUnit.Framework;
using Selenium.Base;
using Selenium.Entities;

namespace Selenium.Tests;

[TestFixture]
public class AuthTest : TestBase
{
    [Test]
    public void AuthorizeTest()
    {
        var account = new Account("eddie.1o1@bk.ru", "121233aa");
        ManageWindowSize(1920, 1040);
        Login(account);
        Thread.Sleep(15000);
    }
}