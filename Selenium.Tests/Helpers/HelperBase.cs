using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Tests.Helpers;

public class HelperBase
{
    protected readonly ApplicationManager _app;
    protected readonly IWebDriver _driver;
    protected readonly WebDriverWait _wait;

    protected HelperBase(ApplicationManager app)
    {
        _app = app;
        _driver = app.Driver;
        _wait = app.Wait;
    }

    public void ManageWindowSize(int width, int height) =>
        _driver.Manage().Window.Size = new System.Drawing.Size(width, height);

    protected IWebElement FindElement(By by) =>
        _driver.FindElement(by);
}

public static class WebElementExtension
{
    public static void Fill(this IWebElement element, string text) =>
        element.SendKeys(text);

    public static string GetRealObjectiveId(this IWebElement element, string attributeName) =>
        element.GetAttribute(attributeName).Remove(0, 5);
}