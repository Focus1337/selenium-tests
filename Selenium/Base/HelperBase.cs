using OpenQA.Selenium;
using Selenium.Helpers;

namespace Selenium.Base;

public class HelperBase
{
    protected ApplicationManager _manager;
    protected IWebDriver _driver;

    public HelperBase(ApplicationManager manager)
    {
        _manager = manager;
        _driver = manager.Driver;
    }
    
    public void ManageWindowSize(int width, int height) =>
        _driver.Manage().Window.Size = new System.Drawing.Size(width, height);

    public void FillFieldById(string elementId, string text) =>
        _driver.FindElement(By.Id(elementId)).SendKeys(text);

    public void FillFieldByCssSelector(string cssSelector, string text) =>
        _driver.FindElement(By.CssSelector(cssSelector)).SendKeys(text);

    public void ClickById(string elementId) =>
        _driver.FindElement(By.Id(elementId)).Click();

    public void ClickByCssSelector(string cssSelector) =>
        _driver.FindElement(By.CssSelector(cssSelector)).Click();
}