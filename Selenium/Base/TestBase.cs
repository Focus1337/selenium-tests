using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Selenium.Entities;

namespace Selenium.Base;

public class TestBase
{
    private IWebDriver _driver = null!;
    public IDictionary<string, object> Vars { get; private set; } = null!;
    private IJavaScriptExecutor _js = null!;

    [SetUp]
    public void SetUp()
    {
        _driver = new ChromeDriver();
        _js = (IJavaScriptExecutor) _driver;
        Vars = new Dictionary<string, object>();
    }

    [TearDown]
    protected void TearDown() =>
        _driver.Quit();

    public void OpenPage(string url) =>
        _driver.Navigate().GoToUrl(url);

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

    public void Login(Account account)
    {
        OpenPage("https://todoist.com/auth/login");
        FillFieldById("element-0", account.Email);
        FillFieldById("element-3", account.Password);

        ClickById("element-3");
        ClickById("element-3");

        ClickByCssSelector(".S7Jh9YX");
    }

    public void AddNewTask(Task task)
    {
        ClickByCssSelector(".plus_add_button");
        // _driver.FindElement(By.CssSelector("button[class='plus_add_button']")).Click();
        ClickByCssSelector(".public-DraftStyleDefault-block");
        {
            var element = _driver.FindElement(By.CssSelector(".notranslate"));
            _js.ExecuteScript(
                $"if(arguments[0].contentEditable === 'true') {{arguments[0].innerText = '<div data-contents=\"true\"><div class=\"\" data-block=\"true\" data-editor=\"a5f1g\" data-offset-key=\"adcnk-0-0\"><div data-offset-key=\"adcnk-0-0\" class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\"><span data-offset-key=\"adcnk-0-0\"><span data-text=\"true\">TEXTEXTEXTEXT</span></span></div></div></div>'}}",
                element);
        }
        ClickByCssSelector(".task_editor__description_field");

        FillFieldByCssSelector(".task_editor__description_field", task.Text);
        ClickByCssSelector(".\\_3d1243b2 > .bbdb467b");
    }

    public void AddNewProject(Project project)
    {
        _driver.FindElement(By.CssSelector(".a8af2163:nth-child(2) > svg")).Click();
        {
            var element = _driver.FindElement(By.CssSelector(".a8af2163:nth-child(2) > svg"));
            Actions builder = new Actions(_driver);
            builder.MoveToElement(element).Perform();
        }
        {
            var element = _driver.FindElement(By.TagName("body"));
            Actions builder = new Actions(_driver);
            builder.MoveToElement(element, 0, 0).Perform();
        }
        _driver.FindElement(By.Id("edit_project_modal_field_name")).SendKeys(project.Title);
        _driver.FindElement(By.CssSelector(".reactist_modal_box__actions")).Click();
        _driver.FindElement(By.CssSelector(".ist_button_red")).Click();
        
    }
}