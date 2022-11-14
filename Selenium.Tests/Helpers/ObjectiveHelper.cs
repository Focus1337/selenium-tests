using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Selenium.Models;

namespace Selenium.Tests.Helpers;

public class ObjectiveHelper : HelperBase
{
    private const string ObjectiveAttribute = "id";//"data-item-id";
    private const string ObjectivesListIdentifier = "items";

    public ObjectiveHelper(ApplicationManager app)
        : base(app)
    {
    }

    public void AddObjective(Objective objective)
    {
        FindElement(FindBy.CssSelector, ".plus_add_button").Click();

        FindElement(FindBy.CssSelector, ".notranslate.public-DraftEditor-content").Click();
        FindElement(FindBy.CssSelector, ".notranslate.public-DraftEditor-content").Fill(objective.Title);

        FindElement(FindBy.CssSelector, ".task_editor__description_field").Click();
        FindElement(FindBy.CssSelector, ".task_editor__description_field").Fill(objective.Text);
        FindElement(FindBy.CssSelector, ".\\_3d1243b2 > .bbdb467b").Click();
    }

    public void DeleteObjective(int taskNumber)
    {
        var realId = GetRealObjectiveId(taskNumber);
        if (!IsObjectiveExists(realId))
            throw new NullReferenceException($"Task with provided {nameof(taskNumber)}: {taskNumber} doesn't exist.");
        
        var element = FindElement(FindBy.CssSelector, $"#task-{realId} .task_list_item__content");
        element.Click();

        FindElement(FindBy.CssSelector, "button[aria-label=\"Другие действия\"]").Click();
        FindElement(FindBy.CssSelector, "button[aria-label=\"Удалить задачу…\"]").Click();
        FindElement(FindBy.CssSelector, ".\\_3d1243b2:nth-child(2)").Click();
    }

    public string GetRealObjectiveId(int taskNumber)
    {
        var tasksList = GetObjectivesList();
//*[@id="task-6350549701"]
// 
        if (taskNumber < 0 && tasksList.Count <= taskNumber)
            throw new ArgumentOutOfRangeException(nameof(taskNumber),
                $"Provided {nameof(taskNumber)} isn't included in segment [0, tasks count)");

        return tasksList[taskNumber].GetAttribute(ObjectiveAttribute).Remove(0, 5);
    }

    public Objective GetLastCreatedObjective()
    {
        var lastElement = GetObjectivesList()[^1];
        var realId = lastElement.GetAttribute(ObjectiveAttribute);

        string ElementText(string className) =>
            lastElement.FindElement(By.XPath($"//div[@id='task-{realId}-content']{className}")).Text;

        return new Objective
        {
            Title = ElementText("/div/div"),
            Text = ElementText("/div[2]")
        };
    }

    public bool IsObjectiveExists(string realId) =>
        GetObjectivesList().Any(element => element.GetAttribute(ObjectiveAttribute).Equals(realId));

    private List<IWebElement> GetObjectivesList() =>
        FindElement(FindBy.ClassName, ObjectivesListIdentifier)
            .FindElements(By.TagName("li"))
            .SkipLast(1)
            .ToList();
}