using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Selenium.Models;

namespace Selenium.Tests.Helpers;

public class ObjectiveHelper : HelperBase
{
    private const string ObjectiveAttribute = "id"; //"data-item-id";
    private const string ObjectivesListIdentifier = "items";

    public ObjectiveHelper(ApplicationManager app)
        : base(app)
    {
    }

    public void AddObjective(Objective objective)
    {
        FindElement(By.CssSelector(".plus_add_button")).Click();

        FindElement(By.CssSelector(".notranslate.public-DraftEditor-content")).Click();
        FindElement(By.CssSelector(".notranslate.public-DraftEditor-content")).Fill(objective.Title);

        FindElement(By.CssSelector(".task_editor__description_field")).Click();
        FindElement(By.CssSelector(".task_editor__description_field")).Fill(objective.Text);
        FindElement(By.CssSelector(".\\_3d1243b2 > .bbdb467b")).Click();
    }

    public void DeleteObjective(int taskNumber)
    {
        var realId = GetRealObjectiveId(taskNumber);
        if (!IsObjectiveExists(realId))
            throw new NullReferenceException($"Task with provided {nameof(taskNumber)}: {taskNumber} doesn't exist.");

        FindElement(By.XPath($"//li[@id='task-{realId}']/div/div[2]/div[2]")).Click();

        FindElement(By.CssSelector("button[aria-label=\"Другие действия\"]")).Click();
        FindElement(By.CssSelector("button[aria-label=\"Удалить задачу…\"]")).Click();
        FindElement(By.CssSelector(".\\_3d1243b2:nth-child(2)")).Click();
    }

    public string GetRealObjectiveId(int taskNumber)
    {
        var tasksList = GetObjectivesList();

        if (taskNumber < 0 && tasksList.Count <= taskNumber)
            throw new ArgumentOutOfRangeException(nameof(taskNumber),
                $"Provided {nameof(taskNumber)} isn't included in segment [0, tasks count)");

        return tasksList[taskNumber].GetRealObjectiveId(ObjectiveAttribute);
    }

    public Objective GetLastCreatedObjective()
    {
        var lastElement = GetObjectivesList()[^1];
        var realId = lastElement.GetRealObjectiveId(ObjectiveAttribute);

        string ElementText(string className) =>
            lastElement.FindElement(By.XPath($"//div[@id='task-{realId}-content']{className}")).Text;

        return new Objective
        {
            Title = ElementText("/div/div"),
            Text = ElementText("/div[2]")
        };
    }

    public bool IsObjectiveExists(string realId) =>
        GetObjectivesList().Any(element => element.GetRealObjectiveId(ObjectiveAttribute).Equals(realId));

    private List<IWebElement> GetObjectivesList() =>
        FindElement(By.ClassName(ObjectivesListIdentifier))
            .FindElements(By.TagName("li"))
            .SkipLast(1)
            .ToList();
}