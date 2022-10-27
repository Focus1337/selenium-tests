using OpenQA.Selenium;

namespace Selenium.Helpers;

public class HelperBase
{
    protected readonly ApplicationManager _app;
    protected readonly IWebDriver _driver;

    protected enum FindBy
    {
        Id,
        CssSelector,
        Name,
        ClassName,
        TagName,
        LinkText
    }

    protected HelperBase(ApplicationManager app)
    {
        _app = app;
        _driver = app.Driver;
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
            _ => throw new InvalidSelectorException("No such selector")
        });
}

public static class WebElementExtension
{
    public static void Fill(this IWebElement element, string text) =>
        element.SendKeys(text);

    public static void Click(this IWebElement element) =>
        element.Click();
}