using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace RozetkaTesting.helpers
{
    internal class WaitHelper
    {
        private static readonly TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(10);

        public static void CheckClicable(IWebElement webElement)
        {
            new WebDriverWait(DriverHolder.GetDriver(), DefaultTimeSpan).Until(
                ExpectedConditions.ElementToBeClickable(webElement));
        }

        public static void CheckVisible(By by)
        {
            new WebDriverWait(DriverHolder.GetDriver(), DefaultTimeSpan).Until(ExpectedConditions.ElementIsVisible(by));
        }

        public static void CustomWaitNotContains(IWebElement element, string word)
        {
            var count = 0;
            while (count < 500)
            {
                if (!element.Text.Contains(word))
                    break;
                count++;
            }
        }
    }
}