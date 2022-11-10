using Xunit;

namespace Selenium.Tests.Tests;

[Collection("Collection")]
public class AccountTests
{
    private readonly TestsFixture _fixture;

    public AccountTests(TestsFixture fixture) =>
        _fixture = fixture;

    [Fact]
    public void Login_AccountExists_ShouldLogin()
    {
        // arrange 

        // act
        _fixture.App.AccountHelper.Login(_fixture.Account);

        // assert
        Assert.True(_fixture.App.AccountHelper.IsLoggedIn(_fixture.Account));
    }
}