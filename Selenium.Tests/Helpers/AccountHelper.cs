using System;
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

    public bool IsLoggedIn(Account account)
    {
        FindElement(By.CssSelector(".settings_avatar")).Click();
        return _wait.Until(drv => drv.FindElement(By.ClassName("user_menu_email"))).Text.Equals(account.Email);
    }

    private bool AreCredentialsWrong() =>
        FindElement(By.CssSelector(".\\_8f5b5f2b")).Text.Contains("Неверный Email или пароль.");
}