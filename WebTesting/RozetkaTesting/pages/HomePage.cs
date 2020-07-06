using OpenQA.Selenium;
using RozetkaTesting.helpers;
using SeleniumExtras.PageObjects;

namespace RozetkaTesting.pages
{
    internal class HomePage
    {
        public HomePage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'user-link')]")]
        public IWebElement LoginLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='header-actions__item header-actions__item_type_cart']")]
        public IWebElement CheckoutsButton { get; set; }

        public void OpenHomePage()
        {
            MethodHelper.OpenUrl("https://rozetka.com.ua/");
        }

        public LoginForm GoToLoginForm()
        {
            LoginLink.Click();
            return new LoginForm();
        }

        public SearchField GoToSearchField()
        {
            return new SearchField();
        }

        public CheckoutsForm GoToCheckoutsForm()
        {
            WaitHelper.CheckClicable(CheckoutsButton);
            MethodHelper.Hover(CheckoutsButton);
            CheckoutsButton.Click();
            return new CheckoutsForm();
        }
    }
}