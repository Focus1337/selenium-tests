using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Helpers;

namespace Selenium;

public class ApplicationManager
{
    public IDictionary<string, object> Vars { get; private set; } = null!;
    private readonly string _baseUrl;
    private StringBuilder _verificationErrors;
    private IJavaScriptExecutor _js;

    public IWebDriver Driver { get; }
    public NavigationHelper NavigationHelper { get; }
    public LoginHelper LoginHelper { get; }
    public TaskHelper TaskHelper { get; }
    public ProjectHelper ProjectHelper { get; }

    public ApplicationManager()
    {
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
        _js = (IJavaScriptExecutor) Driver;
        Vars = new Dictionary<string, object>();
        _verificationErrors = new StringBuilder();
        _baseUrl = "http://localhost";

        NavigationHelper = new NavigationHelper(this, _baseUrl);
        LoginHelper = new LoginHelper(this);
        TaskHelper = new TaskHelper(this);
        ProjectHelper = new ProjectHelper(this);
    }

    public void Stop() => Driver.Quit();
}