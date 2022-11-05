using OpenQA.Selenium;
using Selenium.Entities;

namespace Selenium.Helpers;

public class AccountHelper : HelperBase
{
    public AccountHelper(ApplicationManager app)
        : base(app)
    {
    }

    public void Login(Account account)
    {
        _app.NavigationHelper.OpenPage("/auth/login");
        FindElement(FindBy.Id, "element-0").Fill(account.Email);
        FindElement(FindBy.Id, "element-3").Fill(account.Password);

        FindElement(FindBy.Id, "element-3").Click();
        FindElement(FindBy.Id, "element-3").Click();

        FindElement(FindBy.CssSelector, ".S7Jh9YX").Click();

        var _ = _wait.Until(drv => drv.FindElement(By.Id("left_menu_inner")));

        // if (AreCredentialsWrong())
        //     throw new InvalidCredentialException("Provided wrong email or password credentials.");
    }

    public bool IsLoggedIn(Account account)
    {
        FindElement(FindBy.CssSelector, ".settings_avatar").Click();
        return _wait.Until(drv => drv.FindElement(By.ClassName("user_menu_email"))).Text.Equals(account.Email);
    }

    private bool AreCredentialsWrong() =>
        !FindElement(FindBy.CssSelector, ".\\_8f5b5f2b").Text.Contains("Неверный Email или пароль.");
}