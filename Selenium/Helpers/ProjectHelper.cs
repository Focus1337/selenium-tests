using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using Selenium.Entities;

namespace Selenium.Helpers;

public class ProjectHelper : HelperBase
{
    public ProjectHelper(ApplicationManager app)
        : base(app)
    {
    }

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

    public Project GetLastCreatedProject()
    {
        // FindElement(FindBy.LinkText, "semenar 3").Click();
        FindElement(FindBy.Id, "left_menu_inner").Click();
        // _driver.ExecuteJavaScript("let element = document.getElementById(\"projects_list\"); element.lastElementChild.text;");
        var elements = _driver.FindElements(By.Id("projects_list"));
        var element = elements[^1];
        var getLastChildText = element
            .FindElement(By.ClassName("eYDM03d0TdWUmvQvarxM6w=="))
            .FindElement(By.ClassName("bwinE4g8Ubo+bdRFBhZqsg=="))
            .FindElement(By.TagName("a"))
            .FindElement(By.ClassName("FnFY2YlPR10DcnOkjvMMmA=="))
            .Text;
        return new Project {Title = getLastChildText};
    }
}