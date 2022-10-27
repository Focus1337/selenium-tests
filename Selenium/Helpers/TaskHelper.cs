using OpenQA.Selenium;
using Selenium.Base;
using Selenium.Entities;

namespace Selenium.Helpers;

public class TaskHelper : HelperBase
{
    public TaskHelper(ApplicationManager manager)
        : base(manager) { }
    
    public void AddNewTask(Task task)
    {
        // ClickByCssSelector(".plus_add_button");
        
        // ??
        // _driver.FindElement(By.CssSelector("button[class='plus_add_button']")).Click();
        
        // ClickByCssSelector(".public-DraftStyleDefault-block");
        // {
        //     var element = _driver.FindElement(By.CssSelector(".notranslate"));
        //     _js.ExecuteScript(
        //         $"if(arguments[0].contentEditable === 'true') {{arguments[0].innerText = '<div data-contents=\"true\"><div class=\"\" data-block=\"true\" data-editor=\"a5f1g\" data-offset-key=\"adcnk-0-0\"><div data-offset-key=\"adcnk-0-0\" class=\"public-DraftStyleDefault-block public-DraftStyleDefault-ltr\"><span data-offset-key=\"adcnk-0-0\"><span data-text=\"true\">TEXTEXTEXTEXT</span></span></div></div></div>'}}",
        //         element);
        // }
        // ClickByCssSelector(".task_editor__description_field");
        //
        // FillFieldByCssSelector(".task_editor__description_field", task.Text);
        // ClickByCssSelector(".\\_3d1243b2 > .bbdb467b");
    }
}