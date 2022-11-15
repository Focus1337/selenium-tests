using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Tests.Helpers;

public class HelperBase
{
    protected readonly ApplicationManager _app;
    protected readonly IWebDriver _driver;
    protected readonly WebDriverWait _wait;

    protected enum FindBy
    {
        Id,
        CssSelector,
        Name,
        ClassName,
        TagName,
        LinkText,
        XPath
    }

    protected HelperBase(ApplicationManager app)
    {
        _app = app;
        _driver = app.Driver;
        _wait = app.Wait;
    }

    public void ManageWindowSize(int width, int height) =>
        _driver.Manage().Window.Size = new System.Drawing.Size(width, height);

    protected IWebElement FindElement(FindBy selectionMethod, string selectionText) =>
        _driver.FindElement(selectionMethod switch
        {
            FindBy.Id => By.Id(selectionText),
            FindBy.CssSelector => By.CssSelector(selectionText),
            FindBy.Name => By.Name(selectionText),
            FindBy.ClassName => By.ClassName(selectionText),
            FindBy.TagName => By.TagName(selectionText),
            FindBy.LinkText => By.LinkText(selectionText),
            FindBy.XPath => By.XPath(selectionText),
            _ => throw new InvalidSelectorException("No such selector")
        });
}

public static class WebElementExtension
{
    public static void Fill(this IWebElement element, string text) =>
        element.SendKeys(text);

    public static void Click(this IWebElement element) =>
        element.Click();

    public static string GetRealObjectiveId(this IWebElement element, string attributeName) =>
        element.GetAttribute(attributeName).Remove(0, 5);
}