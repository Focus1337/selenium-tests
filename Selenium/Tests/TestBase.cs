using NUnit.Framework;

namespace Selenium.Tests;

public class TestBase
{
    protected ApplicationManager _app = null!;
    protected const string BaseEmail = "eddie.1o1@bk.ru";
    protected const string BasePassword = "121233aa";

    [SetUp]
    protected void SetUp() =>
        _app = new ApplicationManager();

    [TearDown]
    protected void TearDown() =>
        _app.Stop();
}