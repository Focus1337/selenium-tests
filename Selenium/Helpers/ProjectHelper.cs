using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Selenium.Entities;

namespace Selenium.Helpers;

public class ProjectHelper : HelperBase
{
    public ProjectHelper(ApplicationManager app)
        : base(app)
    {
    }

    public void AddProject(Project project)
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

    public void DeleteProject(int projectNumber)
    {
        var projectsListElements = GetProjectsList();
        var element = projectsListElements[projectNumber];
        element.FindElement(By.TagName("button")).Click();

        FindElement(FindBy.CssSelector, ".menu_item:nth-child(13) > .icon_menu_item__content").Click();
        FindElement(FindBy.CssSelector, ".\\_3d1243b2 > .bbdb467b").Click();
    }

    public Project GetLastCreatedProject() =>
        // DRIVER VERSION:
        new() {Title = GetProjectsList()[^1].FindElement(By.ClassName("FnFY2YlPR10DcnOkjvMMmA==")).Text};
    // JS VERSION
    // var projectsListElement = "let projectList = document.getElementById('projects_list')";
    // var lastChildElement = "projectList.lastElementChild";
    // var childText =
    //     _driver.ExecuteJavaScript<string>($"{projectsListElement}; return {lastChildElement}.innerText;");

    public Project GetProjectByNumber(int projectNumber)
    {
        // DRIVER VERSION:
        var projectsList = GetProjectsList();
        var text = projectsList[projectNumber].FindElement(By.ClassName("FnFY2YlPR10DcnOkjvMMmA==")).Text;

        return new Project {Title = text};

        // JS VERSION
        // var projectsListElement = "let projectList = document.getElementById('projects_list')";
        // var childrenCount =
        //     _driver.ExecuteJavaScript<int>($"{projectsListElement}; return projectList.childElementCount;");
        //
        // if (0 <= projectNumber && projectNumber < childrenCount)
        //     throw new ArgumentOutOfRangeException(nameof(projectNumber),
        //         $"Provided {nameof(projectNumber)} isn't included in segment [0, projects count)");
        //
        // var childElement = $"projectList.children[{projectNumber}]";
        //
        // var childText = _driver.ExecuteJavaScript<string>(
        //     $"{projectsListElement}; return {childElement}.getElementsByClassName('FnFY2YlPR10DcnOkjvMMmA==')[0].textContent;");
        // // либо getElementsByTagName('span')[1], если название класса меняется
        //
        // return new Project {Title = childText};
    }

    public bool IsProjectExists(string realId) =>
        GetProjectsList().Any(x => x.GetAttribute("data-id") == realId);

    public string GetRealProjectId(int projectNumber)
    {
        // DRIVER VERSION:
        var projectsList = GetProjectsList();

        if (projectNumber < 0 && projectsList.Count <= projectNumber)
            throw new ArgumentOutOfRangeException(nameof(projectNumber),
                $"Provided {nameof(projectNumber)} isn't included in segment [0, projects count)");

        return projectsList[projectNumber].GetAttribute("data-id");

        // JS VERSION:
        // var projectsListElement = "let projectList = document.getElementById('projects_list')";
        // var childrenCount =
        //     _driver.ExecuteJavaScript<int>($"{projectsListElement}; return projectList.childElementCount;");
        //
        // if (0 <= projectNumber && projectNumber < childrenCount)
        //     throw new ArgumentOutOfRangeException(nameof(projectNumber),
        //         $"Provided {nameof(projectNumber)} isn't included in segment [0, projects count)");
        //
        // var childElement = $"projectList.children[{projectNumber}]";
        //
        // return _driver.ExecuteJavaScript<int>(
        //     $"{projectsListElement}; return +{childElement}.getAttribute('data-id');");
    }

    private ReadOnlyCollection<IWebElement> GetProjectsList() =>
        FindElement(FindBy.Id, "projects_list")
            .FindElements(By.TagName("li"));
}