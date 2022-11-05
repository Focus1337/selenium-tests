using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Selenium.Entities;

namespace Selenium.Helpers;

public class TaskHelper : HelperBase
{
    private const string TaskAttribute = "data-item-id";
    private const string TasksListIdentifier = "items";

    public TaskHelper(ApplicationManager app)
        : base(app)
    {
    }

    public void AddTask(Task task)
    {
        FindElement(FindBy.CssSelector, ".plus_add_button").Click();

        FindElement(FindBy.CssSelector, ".notranslate.public-DraftEditor-content").Click();
        FindElement(FindBy.CssSelector, ".notranslate.public-DraftEditor-content").Fill(task.Title);

        FindElement(FindBy.CssSelector, ".task_editor__description_field").Click();
        FindElement(FindBy.CssSelector, ".task_editor__description_field").Fill(task.Text);
        FindElement(FindBy.CssSelector, ".\\_3d1243b2 > .bbdb467b").Click();
    }

    public void DeleteTask(int taskNumber)
    {
        var realId = GetRealTaskId(taskNumber);
        if (!IsTaskExists(realId))
            throw new NullReferenceException($"Task with provided {nameof(taskNumber)}: {taskNumber} doesn't exist.");
        
        var element = FindElement(FindBy.CssSelector, $"#task-{realId} .task_list_item__content");
        element.Click();

        FindElement(FindBy.CssSelector, "button[aria-label=\"Другие действия\"]").Click();
        FindElement(FindBy.CssSelector, "button[aria-label=\"Удалить задачу…\"]").Click();
        FindElement(FindBy.CssSelector, ".\\_3d1243b2:nth-child(2)").Click();
    }

    public string GetRealTaskId(int taskNumber)
    {
        var tasksList = GetTasksList();

        if (taskNumber < 0 && tasksList.Count <= taskNumber)
            throw new ArgumentOutOfRangeException(nameof(taskNumber),
                $"Provided {nameof(taskNumber)} isn't included in segment [0, tasks count)");

        return tasksList[taskNumber].GetAttribute(TaskAttribute);
    }

    public Task GetLastCreatedTask()
    {
        var lastElement = GetTasksList()[^1];
        var realId = lastElement.GetAttribute(TaskAttribute);

        string ElementText(string className) =>
            lastElement.FindElement(By.CssSelector($"#task-{realId}-content " + className)).Text;

        return new Task
        {
            Title = ElementText("> .f9408a0e > .task_content"),
            Text = ElementText("> .task_description")
        };
    }

    public bool IsTaskExists(string realId) =>
        GetTasksList().Any(element => element.GetAttribute(TaskAttribute).Equals(realId));

    private List<IWebElement> GetTasksList() =>
        FindElement(FindBy.ClassName, TasksListIdentifier)
            .FindElements(By.TagName("li"))
            .SkipLast(1)
            .ToList();
}