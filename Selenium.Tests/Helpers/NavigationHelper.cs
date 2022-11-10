namespace Selenium.Tests.Helpers;

public class NavigationHelper : HelperBase
{
    private readonly string _baseUrl;

    public NavigationHelper(ApplicationManager app, string baseUrl)
        : base(app) =>
        _baseUrl = baseUrl;

    public void OpenPage(string endpoint) =>
        _driver.Navigate().GoToUrl(_baseUrl + endpoint);

    public void OpenHomePage() =>
        OpenPage("");
}