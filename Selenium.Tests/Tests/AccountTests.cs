using System.Security.Authentication;
using Selenium.Models;
using Xunit;

namespace Selenium.Tests.Tests;

[Collection("Collection")]
public class AccountTests
{
    private readonly TestsFixture _fixture;

    public AccountTests(TestsFixture fixture) =>
        _fixture = fixture;

    [Theory]
    [MemberData(nameof(TestsFixture<Account>.GetData), parameters: 3, MemberType = typeof(TestsFixture<Account>))]
    public void Login_AccountExists_ShouldLogin(string email, string password)
    {
        // arrange 
        var account = new Account { Email = email, Password = password };

        // act
        _fixture.App.AccountHelper.Login(account, true);

        // assert
        Assert.True(_fixture.App.AccountHelper.IsLoggedIn(_fixture.Account));
    }

    [Theory]
    [MemberData(nameof(TestsFixture<Account>.GetData), parameters: 3, MemberType = typeof(TestsFixture<Account>))]
    public void Login_AccountNotExists_ShouldLogin(string email, string password)
    {
        // arrange 
        var account = new Account { Email = email, Password = password };

        // act & assert
        Assert.Throws<InvalidCredentialException>(() => _fixture.App.AccountHelper.Login(account, true));
    }
}