using OpenQA.Selenium;
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

        FindElement(FindBy.CssSelector, ".public-DraftStyleDefault-block").Click();
        {
            var element = FindElement(FindBy.CssSelector, ".notranslate");
            _app.JavaScriptExecutor.ExecuteScript(
                $"if(arguments[0].contentEditable === 'true') {{arguments[0].innerText = '<div data-contents=\"true\"><div class=\"\" data-block=\"true\" data-editor=\"a5f1g\" data-offset-key=\"adcnk-0-0\"><div data-offset-key=\"adcnk-0-0\" class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\"><span data-offset-key=\"adcnk-0-0\"><span data-text=\"true\">TEXTEXTEXTEXT</span></span></div></div></div>'}}",
                element);
        }
        FindElement(FindBy.CssSelector, ".task_editor__description_field").Click();
        FindElement(FindBy.CssSelector, ".task_editor__description_field").Fill(task.Text);
        FindElement(FindBy.CssSelector, ".\\_3d1243b2 > .bbdb467b").Click();
    }
}