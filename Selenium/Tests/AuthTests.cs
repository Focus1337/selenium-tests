using System.Threading;
using Xunit;

namespace Selenium.Tests;

[Collection("Collection")]
public class AuthTests
{
    private readonly TestsFixture _fixture;

    public AuthTests(TestsFixture fixture) =>
        _fixture = fixture;

    [Fact]
    public void EnterExistingAccountAndLogIn_ShouldLogIn()
    {
        _fixture.App.AccountHelper.Login(_fixture.Account);
        Thread.Sleep(15000);
    }
}