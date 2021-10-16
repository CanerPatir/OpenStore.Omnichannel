using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace OpenStore.Omnichannel.Panel.UIAutomation.Steps;

public abstract class BaseStep
{
    protected BaseStep(ScenarioContext scenarioContext)
    {
        ScenarioContext = scenarioContext;
    }

    protected ScenarioContext ScenarioContext { get; }

    protected IWebDriver WebDriver => ScenarioContext["WEB_DRIVER"] as IWebDriver;
}