using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using RozetkaTesting.helpers;
using SeleniumExtras.PageObjects;

namespace RozetkaTesting.pages
{
    internal class SearchField
    {
        public SearchField()
        {
            PageFactory.InitElements(DriverHolder.GetDriver(), this);
        }

        [FindsBy(How = How.Name, Using = "search")]
        public IWebElement SearchForField { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'select-css ng-untouched')]")]
        public IWebElement FilterDropList { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@value, 'cheap')]")]
        public IWebElement ChooseFilterByDescPrice { get; set; }

        [FindsBy(How = How.XPath, Using = "(//div[contains(@class, \"goods-tile__inner\")])[position()<7]")]
        public IList<IWebElement> SearchElementsButtons { get; set; }


        public SearchField SearchProduct(string product)
        {
            SearchForField.EnterText(product);
            MethodHelper.SubmitForm(SearchForField);
            return this;
        }

        public SearchField FilterProductsByPriceAscending()
        {
            WaitHelper.CheckClicable(FilterDropList);
            FilterDropList.Click();
            ChooseFilterByDescPrice.Click();
            return this;
        }

        public void ChooseRandomElementFromPage()
        {
            Assert.AreEqual(6, SearchElementsButtons.Count);
            var index = new Random().Next(SearchElementsButtons.Count);
            SearchElementsButtons[index].Click();
        }
    }
}