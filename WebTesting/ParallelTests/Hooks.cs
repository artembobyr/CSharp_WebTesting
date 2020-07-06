using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace ParallelTests
{
    public class Hooks : Base
    {
        private readonly BrowserTypeEnum _browserType;

        public Hooks(BrowserTypeEnum browser)
        {
            _browserType = browser;
        }

        [FindsBy(How = How.Name, Using = "q")] private IWebElement InputField { get; set; }

        [FindsBy(How = How.Name, Using = "btnK")]
        private IWebElement Button { get; set; }

        [SetUp]
        public void Init()
        {
            ChooseBrowser(_browserType);
            WebDriver.Navigate().GoToUrl("https://www.google.com.ua");
            PageFactory.InitElements(WebDriver, this);
        }

        [TearDown]
        public void CleanUp()
        {
            WebDriver.Quit();
        }

        protected void SendSomething(string str)
        {
            InputField.SendKeys(str);
            new WebDriverWait(WebDriver, new TimeSpan(0, 0, 10)).Until(ExpectedConditions.ElementToBeClickable(Button))
                .Click();
            Assert.That(WebDriver.PageSource.Contains(str));
        }

        private void ChooseBrowser(BrowserTypeEnum browserType)
        {
            if (browserType == BrowserTypeEnum.Chrome)
            {
                var option = new ChromeOptions();
                option.AddArgument("--headless");
                WebDriver = new ChromeDriver(option);
            }

            if (browserType == BrowserTypeEnum.Firefox)
            {
                var option = new FirefoxOptions();
                option.AddArgument("--headless");
                WebDriver = new FirefoxDriver(option);
            }
        }
    }
}