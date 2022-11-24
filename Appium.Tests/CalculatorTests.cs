using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Appium.Tests;

public class CalculatorTests
{
    /*
     * HOW TO INSTALL & USE:
     * 1. Enable Windows Development mode: https://consumer.huawei.com/en/support/content/en-us15594140/
     * 2. DL & Install WinAppDriver: https://github.com/microsoft/WinAppDriver/releases/download/v1.2.1/WindowsApplicationDriver_1.2.1.msi
     * 3. Add package from NuGet: Microsoft.WinAppDriver.Appium.WebDriver
     * 4. Run tests
     * Useful link: https://github.com/Microsoft/WinAppDriver
     */
    
    private WindowsDriver<WindowsElement> _driver = null!;
    private DesiredCapabilities _capabilities = null!;
    private Process _driverProcess = null!;

    [SetUp]
    public void Setup()
    {
        _driverProcess = Process.Start(@"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe");
        
        _capabilities = new DesiredCapabilities();
        _capabilities.SetCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

        _driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), _capabilities);
    }

    [Test]
    public void Addition_TwoArguments_ShouldCalculateSum()
    {
        _driver.FindElementByAccessibilityId("num5Button").Click();
        _driver.FindElementByAccessibilityId("plusButton").Click();
        _driver.FindElementByAccessibilityId("num3Button").Click();
        _driver.FindElementByAccessibilityId("equalButton").Click();
    }

    [TearDown]
    public void Teardown()
    {
        _driverProcess.Kill();
        _driver.Quit();
    }
}