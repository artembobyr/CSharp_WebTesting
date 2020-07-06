using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlow
{
    [Binding]
    public class LoginSteps
    {
        private IWait<IWebDriver> _defaultWait;
        private IWebDriver _webDriver;

        [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'user-link')]")]
        public IWebElement LoginLink { get; set; }

        [FindsBy(How = How.Id, Using = "auth_email")]
        public IWebElement EmailField { get; set; }

        [FindsBy(How = How.Id, Using = "auth_pass")]
        public IWebElement PasswordField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'auth-modal__submit')]")]
        public IWebElement LoginButton { get; set; }


        [BeforeScenario]
        public void InitBrowser()
        {
            _webDriver = new ChromeDriver();
            _defaultWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
        }

        [AfterScenario]
        public void CleanUp()
        {
            _webDriver.Quit();
        }


        [Given(@"I go to (.*)")]
        public void GivenIGoToRozetka_Com_Ua(string url)
        {
            if (!url.StartsWith("http") && !url.StartsWith("https"))
                url = "https://" + url;
            _webDriver.Navigate().GoToUrl(url);
            _webDriver.Manage().Window.Maximize();
            PageFactory.InitElements(_webDriver, this);
        }

        [Given(@"I click the Log in to your account button")]
        public void GivenIClickTheButton()
        {
            LoginLink.Click();
        }

        [Given(@"I input valid email and valid password into fields")]
        [Scope(Scenario = "Login with valid email and password")]
        public void GivenIInputValidEmailAndValidPasswordIntoFields(Table table)
        {
            var data = table.CreateDynamicSet();
            _defaultWait.Until(ExpectedConditions.ElementToBeClickable(EmailField));
            foreach (var item in data)
            {
                EmailField.SendKeys((string)item.email);
                PasswordField.SendKeys((string)item.password);
            }
        }

        [Then(@"I see username on the page")]
        public void ThenISeeUsernameOnThePage(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _defaultWait.Until(ExpectedConditions.TextToBePresentInElement(LoginLink, (string)data.username));
            Assert.IsNotNull(LoginLink.Text, "Text isn't found");
            Assert.IsTrue(_webDriver.PageSource.Contains(data.username));
        }


        // 
        //[Given(@"I input valid email (.*) and valid password (.*) into fields")]
        //public void GivenIInputValidEmailAndValidPassword(string email, string password)
        //{
        //    _defaultWait.Until(ExpectedConditions.ElementToBeClickable(EmailField));
        //    EmailField.SendKeys(email);
        //    PasswordField.SendKeys(password);
        //}

        [When(@"I click the ""Log in"" button")]
        public void WhenIClickTheButton()
        {
            LoginButton.Submit();
        }

        //[Then(@"I see (.*) on the page")]
        //public void ThenISeeLoginOnThePage(string username)
        //{
        //    _defaultWait.Until(ExpectedConditions.TextToBePresentInElement(LoginButton, username));
        //    Assert.That(_webDriver.PageSource.Contains(username), Is.EqualTo(true), "Username isn't found");
        //}

        [Given(@"I input valid (.*) and invalid (.*) into fields")]
        public void GivenIInputValidEmailAndInvalidPassword(string email, string password)
        {
            //вызов другого степа
            //Given("I click the Log in to your account button");
            LoginLink.Click();
            _defaultWait.Until(ExpectedConditions.ElementToBeClickable(EmailField));
            EmailField.SendKeys(email);
            PasswordField.SendKeys(password);
        }

        [Then(@"I see message (.*)")]
        public void ThenISeeMessageAboutInvalidPassword(string message)
        {
            _defaultWait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.error-message")));
            Assert.IsTrue(_webDriver.PageSource.Contains(message), "Error message isn't shown");
        }
    }
}
