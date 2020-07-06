using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace RozetkaTesting.helpers
{
    internal static class DriverHolder
    {
        //private static IWebDriver driver;
        private static IWebDriver Driver { get; set; }

        public static void Initialize()
        {
            var browser = ConfigurationManager.AppSettings["browser"];
            switch (browser)
            {
                case "Chrome":
                    Driver = new ChromeDriver();
                    break;
                case "Firefox":
                    Driver = new FirefoxDriver();
                    break;
            }
            Driver.Manage().Window.Maximize();
        }

        internal static IWebDriver GetDriver()
        {
            return Driver;
        }
    }
}