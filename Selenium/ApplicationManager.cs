﻿using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.Helpers;

namespace Selenium;

public class ApplicationManager : IDisposable
{
    private const string BaseUrl = "https://todoist.com/";
    private static ThreadLocal<ApplicationManager> _app = new();
    public IWebDriver Driver { get; }
    public NavigationHelper NavigationHelper { get; }
    public AccountHelper AccountHelper { get; }
    public TaskHelper TaskHelper { get; }
    public ProjectHelper ProjectHelper { get; }
    public IJavaScriptExecutor JavaScriptExecutor { get; }
    public WebDriverWait Wait { get; }
    
    public ApplicationManager()
    {
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        JavaScriptExecutor = (IJavaScriptExecutor) Driver;

        NavigationHelper = new NavigationHelper(this, BaseUrl);
        AccountHelper = new AccountHelper(this);
        TaskHelper = new TaskHelper(this);
        ProjectHelper = new ProjectHelper(this);
    }

    public static ApplicationManager GetInstance()
    {
        if (!_app.IsValueCreated)
        {
            var newInstance = new ApplicationManager();
            newInstance.NavigationHelper.OpenHomePage();
            _app.Value = newInstance;
        }

        return _app.Value!;
    }

    public void Dispose()
    {
        try
        {
            Driver.Quit();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}