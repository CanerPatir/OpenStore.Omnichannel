using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Xunit;

namespace OpenStore.Omnichannel.Panel.UIAutomation.Steps;

[Binding]
public sealed class CreateProduct : BaseStep
{
    public CreateProduct(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    private readonly TestData _testData = new();

    private IWebElement ContactSection => WebDriver.FindElement(By.XPath(".//div[@class='row contact']//div[@class='col-sm-5']"));

    //Contact form
    private static By FormSectionBy => By.XPath("//form");
    private IWebElement FormSection => ContactSection.FindElement(FormSectionBy);
    private IWebElement NameTextBox => FormSection.FindElement(By.Id("name"));
    private IWebElement EmailTextBox => FormSection.FindElement(By.Id("email"));
    private IWebElement PhoneTextBox => FormSection.FindElement(By.Id("phone"));
    private IWebElement SubjectTextBox => FormSection.FindElement(By.Id("subject"));
    private IWebElement MessageTextBox => FormSection.FindElement(By.Id("description"));
    private IWebElement SubmitButton => FormSection.FindElement(By.Id("submitContact"));

    //Confirmation message
    private IWebElement HeaderText => ContactSection.FindElement(By.XPath(".//h2"));
    private IReadOnlyCollection<IWebElement> ParagraphText => ContactSection.FindElements(By.XPath(".//p"));
        
    [When(@"I submit the following contact details (.*), (.*), (.*), (.*) and (.*)")]
    public void WhenISubmitTheFollowingContactDetailsTestTestTest_ComTestingAndHelloWorldCanIBookARoomPlease(string name, string email, string phone, string subject,
        string message)
    {
        _testData.MyMessage.Name = name;
        _testData.MyMessage.Email = email;
        _testData.MyMessage.PhoneNumber = phone;
        _testData.MyMessage.Subject = subject;
        _testData.MyMessage.Message = message;

        NameTextBox.SendKeys(_testData.MyMessage.Name);
        EmailTextBox.SendKeys(_testData.MyMessage.Email);
        PhoneTextBox.SendKeys(_testData.MyMessage.PhoneNumber);
        SubjectTextBox.SendKeys(_testData.MyMessage.Subject);
        MessageTextBox.SendKeys(_testData.MyMessage.Message);

        WebDriver.ScrollToElement(SubmitButton);
        SubmitButton.Click();
    }

    [When(@"I submit some details in the contact details form")]
    public void WhenISubmitSomeDetailsInTheContactDetailsForm()
    {
        _testData.MyMessage.Name = "";
        _testData.MyMessage.Email = "";
        _testData.MyMessage.PhoneNumber = "";
        _testData.MyMessage.Subject = "";
        _testData.MyMessage.Message = "";

        NameTextBox.SendKeys(_testData.MyMessage.Name);
        EmailTextBox.SendKeys(_testData.MyMessage.Email);
        PhoneTextBox.SendKeys(_testData.MyMessage.PhoneNumber);
        SubjectTextBox.SendKeys(_testData.MyMessage.Subject);
        MessageTextBox.SendKeys(_testData.MyMessage.Message);

        WebDriver.ScrollToElement(SubmitButton);
        SubmitButton.Click();
    }

    [Then(@"I should be told that the form was submitted")]
    public void ThenIShouldBeToldThatTheFormWasSubmitted()
    {
        Assert.True(WebDriver.Exists(FormSectionBy));
        Thread.Sleep(2000);

        Assert.Equal($"Thanks for getting in touch {_testData.MyMessage.Name}!", HeaderText.Text);

        var paragraph = "";
        foreach (var p in ParagraphText)
            paragraph += p.Text + " ";

        Assert.Equal($"We'll get back to you about {_testData.MyMessage.Subject} as soon as possible. ", paragraph);
    }

}