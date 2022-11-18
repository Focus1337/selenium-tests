using System.Security.Authentication;
using OpenQA.Selenium;
using Selenium.Models;

namespace Selenium.Tests.Helpers;

public class AccountHelper : HelperBase
{
    public AccountHelper(ApplicationManager app)
        : base(app)
    {
    }

    public void Login(Account account)
    {
        if (IsLoggedIn(account))
            return;

        _app.NavigationHelper.OpenPage("/auth/login");
        FindElement(By.Id("element-0")).Fill(account.Email);
        FindElement(By.Id("element-3")).Fill(account.Password);

        FindElement(By.Id("element-3")).Click();
        FindElement(By.Id("element-3")).Click();

        FindElement(By.CssSelector(".S7Jh9YX")).Click();

        try
        {
            var _ = _wait.Until(drv => drv.FindElement(By.Id("left_menu_inner")));
        }
        catch (WebDriverTimeoutException)
        {
            if (AreCredentialsWrong())
                throw new InvalidCredentialException("Provided wrong email or password credentials.");
        }
    }

    private void Logout()
    {
        FindElement(By.CssSelector(".settings_avatar")).Click();
        FindElement(By.XPath("//div[2]/div/div/button[2]")).Click();
    }

    public bool IsLoggedIn(Account account)
    {
        IWebElement element;
        try
        {
            element = FindElement(By.CssSelector(".settings_avatar"));
        }
        catch (NoSuchElementException)
        {
            return false;
        }

        element.Click();
        var elementExists = _wait.Until(drv => drv.FindElement(By.ClassName("user_menu_email"))).Text
            .Equals(account.Email);

        _app.NavigationHelper.OpenHomePage();
        var _ = _wait.Until(drv => drv.FindElement(By.Id("left_menu_inner")));

        return elementExists;
    }

    private bool AreCredentialsWrong() =>
        FindElement(By.CssSelector(".\\_8f5b5f2b")).Text.Contains("Неверный Email или пароль.");
}