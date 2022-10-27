namespace Selenium.Entities;

public class Account
{
    public Account(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}