using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace RozetkaTesting.helpers
{
    public static class MethodHelper
    {
        public static void OpenUrl(string url)
        {
            DriverHolder.GetDriver().Navigate().GoToUrl(url);
        }

        public static void Hover(IWebElement webElement)
        {
            new Actions(DriverHolder.GetDriver()).MoveToElement(webElement).Build().Perform();
        }

        public static void EnterText(this IWebElement webElement, string value)
        {
            webElement.SendKeys(value);
        }

        public static void SubmitForm(IWebElement webElement)
        {
            webElement.SendKeys(Keys.Enter);
        }
    }
}