using OpenQA.Selenium.Interactions;
using Selenium.Entities;

namespace Selenium.Helpers;

public class ProjectHelper : HelperBase
{
    public ProjectHelper(ApplicationManager app)
        : base(app)
    { }

    public void AddNewProject(Project project)
    {
        FindElement(FindBy.CssSelector, ".a8af2163:nth-child(2) > svg").Click();
        {
            var element = FindElement(FindBy.CssSelector, ".a8af2163:nth-child(2) > svg");
            var builder = new Actions(_driver);
            builder.MoveToElement(element).Perform();
        }
        {
            var element = FindElement(FindBy.TagName, "body");
            var builder = new Actions(_driver);
            builder.MoveToElement(element, 0, 0).Perform();
        }

        FindElement(FindBy.Id, "edit_project_modal_field_name").Fill(project.Title);
        FindElement(FindBy.CssSelector, ".reactist_modal_box__actions").Click();
        FindElement(FindBy.CssSelector, ".ist_button_red").Click();
    }
}