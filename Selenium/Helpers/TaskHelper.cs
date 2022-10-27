using Selenium.Entities;

namespace Selenium.Helpers;

public class TaskHelper : HelperBase
{
    public TaskHelper(ApplicationManager app)
        : base(app)
    { }

    public void AddNewTask(Task task)
    {
        FindElement(FindBy.CssSelector, ".plus_add_button").Click();

        FindElement(FindBy.CssSelector, ".notranslate.public-DraftEditor-content").Click();
        FindElement(FindBy.CssSelector, ".notranslate.public-DraftEditor-content").Fill(task.Title);

        FindElement(FindBy.CssSelector, ".task_editor__description_field").Click();
        FindElement(FindBy.CssSelector, ".task_editor__description_field").Fill(task.Text);
        FindElement(FindBy.CssSelector, ".\\_3d1243b2 > .bbdb467b").Click();
    }
}