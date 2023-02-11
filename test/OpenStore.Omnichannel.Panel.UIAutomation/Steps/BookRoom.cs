using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using Xunit;

namespace OpenStore.Omnichannel.Panel.UIAutomation.Steps;

[Binding]
public sealed class BookRoom : BaseStep
{
    private readonly TestData _testData = new TestData();


    public BookRoom(ScenarioContext scenarioContext) : base(scenarioContext)
    {
    }

    private IWebElement BookRoomButton => WebDriver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right openBooking']"));
    private ReadOnlyCollection<IWebElement> RoomInformation => WebDriver.FindElements(By.XPath("//div[@class='row hotel-room-info']"));
    private int _initialRoomSectionsOnPage = 0;

    private IWebElement Calendar => WebDriver.FindElement(By.XPath("//div[@class='rbc-calendar']"));
    private IEnumerable<IWebElement> CalendarRows => Calendar.FindElements(By.XPath(".//div[@class='rbc-month-row']"));
    private By DaysInWeek => By.XPath(".//div[@class='rbc-day-bg']");

    private IWebElement FirstNameTextBox => WebDriver.FindElement(By.Name("firstname"));
    private IWebElement LastNameTextBox => WebDriver.FindElement(By.Name("lastname"));
    private IWebElement EmailTextBox => WebDriver.FindElement(By.Name("email"));
    private IWebElement PhoneTextBox => WebDriver.FindElement(By.Name("phone"));
    private IWebElement SubmitBookingButton => WebDriver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right book-room']"));
    private IWebElement NextMonth => WebDriver.FindElement(By.XPath("//button[text()='Next']"));
    private IWebElement SuccessfulBookingMessage => WebDriver.FindElement(By.XPath("//div[@class='form-row']//h3"));

    [Given(@"at least 1 room exists in the hotel")]
    public void GivenAtLeastRoomExistsInTheHotel()
    {
        Assert.True(RoomInformation.Count > 0, "No rooms currently exist in the hotel");
    }

    [When(@"I click on the book a room button")]
    public void WhenIClickOnTheBookARoomButton()
    {
        _initialRoomSectionsOnPage = RoomInformation.Count; //When room section expands, an additional section is created on the page
        BookRoomButton.Click();
    }

    [Then(@"the room info section should appear")]
    public void ThenTheRoomInfoSectionShouldAppear()
    {
        Assert.True(RoomInformation.Count > _initialRoomSectionsOnPage);
    }


    [When(@"I select a date range")]
    public void WhenISelectADateRange()
    {
        var dateSet = false;
        var available = false;

        while (!dateSet)
        {
            foreach (var week in CalendarRows)
            {
                try
                {
                    available = !week.FindElement(By.XPath(".//div[@title='Unavailable']")).Displayed;
                }
                catch (NoSuchElementException)
                {
                    available = true;
                }
                    
                //Only book week if there are more than 1 day within the current month, and the week is available
                if (available && week.FindElements(DaysInWeek).Count > 1)
                {
                    WebDriver.ScrollToElement(week);
                    SelectDateRangeInWeek(week);
                    dateSet = true;
                    break;
                }
            }

            NextMonth.Click();
        }
    }

    private void SelectDateRangeInWeek(IWebElement week)
    {
        var days = week.FindElements(DaysInWeek);
        var day1 = days[0];
        var day2 = days[^1];

        var action = new Actions(WebDriver);

        action.ClickAndHold(day2);
        action.MoveToElement(day2);
        action.MoveToElement(day1);
        action.DragAndDrop(day1, day2);
        action.Perform();
    }

    [When(@"I enter valid room booking details")]
    public void WhenIEnterValidRoomBookingDetails()
    {
        FirstNameTextBox.SendKeys("Firstname");
        LastNameTextBox.SendKeys("Lastname");
        EmailTextBox.SendKeys("email@test.com");
        PhoneTextBox.SendKeys("01234567890");
        SubmitBookingButton.Click();
    }

    [Then(@"I should see the successful booking message")]
    public void ThenIShouldSeeTheSuccessfulBookingMessage()
    {
        Assert.Equal("Booking Successful!", SuccessfulBookingMessage.Text);
    }
}