using System.Threading;
using NUnit.Framework;
using Selenium.Entities;

namespace Selenium.Tests;

[TestFixture]
public class AuthTest : TestBase
{
    [Test]
    public void EnterExistingAccountAndLogIn_ShouldLogIn()
    {
        var account = new Account(BaseEmail, BasePassword);
        
        _app.LoginHelper.Login(account);
        Thread.Sleep(15000);
    }
}