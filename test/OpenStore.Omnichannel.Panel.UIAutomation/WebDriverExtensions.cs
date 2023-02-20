using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OpenStore.Omnichannel.Panel.UIAutomation;

public static class WebDriverExtensions
{
    public static void ScrollToElement(this IWebDriver driver, IWebElement element)
    {
        var yPos = element.Location.Y;
        var windowSize = driver.Manage().Window.Size.Height;
        var scrollPosition = yPos - windowSize / 2;
        ((IJavaScriptExecutor) driver).ExecuteScript("window.scrollTo(0, arguments[0]);", scrollPosition);
    }

    public static bool Exists(this IWebDriver driver, By elementBy)
    {
        try
        {
            return driver.FindElement(elementBy).Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    public static bool Exists(this IWebElement baseElement, By elementBy)
    {
        try
        {
            return baseElement.FindElement(elementBy).Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}