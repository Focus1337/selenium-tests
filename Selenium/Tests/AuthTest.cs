using System.Threading;
using NUnit.Framework;
using Selenium.Base;
using Selenium.Entities;
using Selenium.Helpers;

namespace Selenium.Tests;

[TestFixture]
public class AuthTest : TestBase
{
    [Test]
    public void AuthorizeTest()
    {
        var account = new Account("eddie.1o1@bk.ru", "121233aa");
        
        _app.LoginHelper.Login(account);
        Thread.Sleep(15000);
    }
}