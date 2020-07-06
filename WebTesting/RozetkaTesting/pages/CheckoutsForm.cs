using System.Threading;
using OpenQA.Selenium;
using RozetkaTesting.helpers;
using SeleniumExtras.PageObjects;

namespace RozetkaTesting.pages
{
    internal class CheckoutsForm
    {
        public CheckoutsForm()
        {
            PageFactory.InitElements(DriverHolder.GetDriver(), this);
        }

        [FindsBy(How = How.CssSelector, Using = "button.cart-modal__remove")]
        private IWebElement DeleteButton { get; set; }

        [FindsBy(How = How.CssSelector,
            Using = "a.cart-modal__actions-control.cart-modal__actions-control_type_remove > span")]
        private IWebElement ConfirmDeleteButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='cart-modal__inner']")]
        private IWebElement CheckoutsHeader { get; set; }

        private By CheckoutsHeaderLocator => By.XPath("//*[@class='cart-modal__inner'][1]");

        private void DeleteItem()
        {
            MethodHelper.Hover(DeleteButton);
            WaitHelper.CheckClicable(DeleteButton);
            DeleteButton.Click();
            MethodHelper.Hover(ConfirmDeleteButton);
            WaitHelper.CheckClicable(ConfirmDeleteButton);
            ConfirmDeleteButton.Click();
        }

        public void DeleteItems()
        {
            WaitHelper.CheckVisible(CheckoutsHeaderLocator);
            while (!CheckoutsHeader.Text.ToLower().Contains("пуста"))
            {
                DeleteItem();
                Thread.Sleep(1000);
            }
        }
    }
}