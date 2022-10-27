using NUnit.Framework;

namespace Selenium.Base;

public class TestBase
{
    protected ApplicationManager _app = null!;

    [SetUp]
    public void SetUp() =>
        _app = new ApplicationManager();

    [TearDown]
    protected void TearDown() =>
        _app.Stop();
}