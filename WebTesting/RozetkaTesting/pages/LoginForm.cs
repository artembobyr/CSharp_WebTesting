using OpenQA.Selenium;
using RozetkaTesting.helpers;
using SeleniumExtras.PageObjects;

namespace RozetkaTesting.pages
{
    internal class LoginForm
    {
        public LoginForm()
        {
            PageFactory.InitElements(DriverHolder.GetDriver(), this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='header-topline__user js-rz-auth']")]
        public IWebElement AfterLogin { get; set; }

        [FindsBy(How = How.Id, Using = "auth_email")]
        private IWebElement EmailField { get; set; }

        [FindsBy(How = How.Id, Using = "auth_pass")]
        private IWebElement PasswordField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'auth-modal__submit')]")]
        private IWebElement LoginButton { get; set; }

        public void DoLogin(string email, string password)
        {
            EmailField.EnterText(email);
            PasswordField.EnterText(password);
            LoginButton.Click();
        }
    }
}