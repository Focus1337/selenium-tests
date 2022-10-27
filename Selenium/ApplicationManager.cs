using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Helpers;

namespace Selenium;

public class ApplicationManager
{
    public IDictionary<string, object> Vars { get; private set; }
    private StringBuilder _verificationErrors;
    private const string BaseUrl = "https://todoist.com/";

    public IWebDriver Driver { get; }
    public NavigationHelper NavigationHelper { get; }
    public LoginHelper LoginHelper { get; }
    public TaskHelper TaskHelper { get; }
    public ProjectHelper ProjectHelper { get; }
    public IJavaScriptExecutor JavaScriptExecutor { get; }

    public ApplicationManager()
    {
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
        JavaScriptExecutor = (IJavaScriptExecutor) Driver;
        Vars = new Dictionary<string, object>();
        _verificationErrors = new StringBuilder();

        NavigationHelper = new NavigationHelper(this, BaseUrl);
        LoginHelper = new LoginHelper(this);
        TaskHelper = new TaskHelper(this);
        ProjectHelper = new ProjectHelper(this);
    }

    public void Stop() => Driver.Quit();
}