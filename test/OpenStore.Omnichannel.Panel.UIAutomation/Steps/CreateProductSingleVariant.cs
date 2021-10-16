using TechTalk.SpecFlow;

namespace OpenStore.Omnichannel.Panel.UIAutomation.Steps;

[Binding]
public class CreateProductSingleVariant : BaseStep
{
    public CreateProductSingleVariant(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    [Given(@"The product should be created with the ""(.*)""")]
    public void GivenTheProductShouldBeCreatedWithThe(string p0)
    {
        ScenarioContext.StepIsPending();
    }
        
    [Given(@"should not be checked multiple variant CheckBox")]
    public void GivenShouldNotBeCheckedMultipleVariantCheckBox()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"I click the save button")]
    public void WhenIClickTheSaveButton()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"I should see the success toast message")]
    public void ThenIShouldSeeTheSuccessToastMessage()
    {
        ScenarioContext.StepIsPending();
    }

}