using Selenium.Base;

namespace Selenium.Helpers;

public class NavigationHelper : HelperBase
{
    private string _baseUrl;

    public NavigationHelper(ApplicationManager manager, string baseUrl) : base(manager) =>
        _baseUrl = baseUrl;

    public void OpenPage(string url) =>
        _driver.Navigate().GoToUrl(url);
}