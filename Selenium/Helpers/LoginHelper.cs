using Selenium.Base;
using Selenium.Entities;

namespace Selenium.Helpers;

public class LoginHelper : HelperBase
{
    public LoginHelper(ApplicationManager manager) : base(manager)
    {
    }

    public void Login(Account account)
    {
        _manager.NavigationHelper.OpenPage("https://todoist.com/auth/login");
        FillFieldById("element-0", account.Email);
        FillFieldById("element-3", account.Password);

        ClickById("element-3");
        ClickById("element-3");

        ClickByCssSelector(".S7Jh9YX");
    }
}