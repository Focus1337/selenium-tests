using Selenium.Entities;

namespace Selenium.Helpers;

public class LoginHelper : HelperBase
{
    public LoginHelper(ApplicationManager app) 
        : base(app)
    { }

    public void Login(Account account)
    {
        _app.NavigationHelper.OpenPage("/auth/login");
        FindElement(FindBy.Id, "element-0").Fill(account.Email);
        FindElement(FindBy.Id, "element-3").Fill(account.Password);

        FindElement(FindBy.Id, "element-3").Click();
        FindElement(FindBy.Id, "element-3").Click();

        FindElement(FindBy.CssSelector, ".S7Jh9YX").Click();
    }
}
