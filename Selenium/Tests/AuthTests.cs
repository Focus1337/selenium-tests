using Xunit;

namespace Selenium.Tests;

[Collection("Collection")]
public class AuthTests
{
    private readonly TestsFixture _fixture;

    public AuthTests(TestsFixture fixture) =>
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