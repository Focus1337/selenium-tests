﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Selenium.Models;

namespace Selenium.Tests.Helpers;

public class ProjectHelper : HelperBase
{
    private const string ProjectAttribute = "data-id";
    private const string ProjectsListIdentifier = "projects_list";

    public ProjectHelper(ApplicationManager app)
        : base(app)
    {
    }

    public void AddProject(Project project)
    {
        const string buttonPath = "//div[@id='left_menu_inner']/div/div/div/div/button";
        FindElement(By.XPath(buttonPath)).Click();
        {
            var element = FindElement(By.XPath(buttonPath));
            var builder = new Actions(_driver);
            builder.MoveToElement(element).Perform();
        }
        {
            var element = FindElement(By.TagName("body"));
            var builder = new Actions(_driver);
            builder.MoveToElement(element, 0, 0).Perform();
        }

        FindElement(By.Id("edit_project_modal_field_name")).Fill(project.Title);
        FindElement(By.CssSelector(".reactist_modal_box__actions")).Click();
        FindElement(By.CssSelector(".ist_button_red")).Click();
    }

    public void DeleteProject(int projectNumber)
    {
        var projectsListElements = GetProjectsList();

        if (!IsProjectExists(GetRealProjectId(projectNumber)))
            throw new NullReferenceException(
                $"Project with provided {nameof(projectNumber)}: {projectNumber} doesn't exist.");

        var element = projectsListElements[projectNumber];
        element.FindElement(By.TagName("button")).Click();

        FindElement(By.CssSelector(".menu_item:nth-child(13) > .icon_menu_item__content")).Click();
        FindElement(By.CssSelector(".\\_3d1243b2 > .bbdb467b")).Click();
    }

    public void UpdateProjectTitle(int projectNumber, string title)
    {
        var realId = GetRealProjectId(projectNumber);
        if (!IsProjectExists(realId))
            throw new NullReferenceException(
                $"Project with provided {nameof(projectNumber)}: {projectNumber} doesn't exist.");

        var element = FindElement(By.CssSelector($"li[{ProjectAttribute}='{realId}']"));
        element.FindElement(By.TagName("button")).Click();
        FindElement(By.CssSelector(".menu_item:nth-child(4) > .icon_menu_item__content")).Click();

        var field = FindElement(By.Id("edit_project_modal_field_name"));
        field.Clear();
        field.Fill(title);

        FindElement(By.CssSelector(".ist_button_red")).Click();
    }

    public Project GetLastCreatedProject() =>
        new() { Title = GetProjectsList()[^1].FindElement(By.ClassName("FnFY2YlPR10DcnOkjvMMmA==")).Text };

    public Project GetProjectByNumber(int projectNumber)
    {
        var projectsList = GetProjectsList();
        var text = projectsList[projectNumber].FindElement(By.ClassName("FnFY2YlPR10DcnOkjvMMmA==")).Text;

        return new Project { Title = text };
    }

    public bool IsProjectExists(string realId) =>
        GetProjectsList().Any(x => x.GetAttribute(ProjectAttribute) == realId);

    public string GetRealProjectId(int projectNumber)
    {
        var projectsList = GetProjectsList();

        if (projectNumber < 0 && projectsList.Count <= projectNumber)
            throw new ArgumentOutOfRangeException(nameof(projectNumber),
                $"Provided {nameof(projectNumber)} isn't included in segment [0, projects count)");

        return projectsList[projectNumber].GetAttribute(ProjectAttribute);
    }

    private ReadOnlyCollection<IWebElement> GetProjectsList() =>
        FindElement(By.Id(ProjectsListIdentifier))
            .FindElements(By.TagName("li"));
}