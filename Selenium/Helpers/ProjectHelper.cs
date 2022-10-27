using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Selenium.Base;
using Selenium.Entities;

namespace Selenium.Helpers;

public class ProjectHelper : HelperBase
{
    public ProjectHelper(ApplicationManager manager)
        : base(manager)
    {
    }

    public void AddNewProject(Project project)
    {
        ClickByCssSelector(".a8af2163:nth-child(2) > svg");
        {
            var element = _driver.FindElement(By.CssSelector(".a8af2163:nth-child(2) > svg"));
            var builder = new Actions(_driver);
            builder.MoveToElement(element).Perform();
        }
        {
            var element = _driver.FindElement(By.TagName("body"));
            var builder = new Actions(_driver);
            builder.MoveToElement(element, 0, 0).Perform();
        }
        _driver.FindElement(By.Id("edit_project_modal_field_name")).SendKeys(project.Title);
        ClickByCssSelector(".reactist_modal_box__actions");
        ClickByCssSelector(".ist_button_red");
    }
}