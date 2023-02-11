using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace OpenStore.Omnichannel.Panel.UIAutomation.Steps;

[Binding]
public class TestHooks
{
    private const int Timeout = 20;

    [Before]
    public void CreateWebDriver(ScenarioContext context)
    {
        // We are using Chrome, but you can use any driver
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-notifications");

        IWebDriver webDriver = new ChromeDriver(options);
        context["WEB_DRIVER"] = webDriver;
            
        webDriver.Navigate().GoToUrl("https://aw1.automationintesting.online/");
        webDriver.Manage().Window.Maximize();
        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Timeout);
    }

    [After]
    public void CloseWebDriver(ScenarioContext context)
    {
        var driver = context["WEB_DRIVER"] as IWebDriver;
        // driver.Quit();
        driver.Close();
    }
}