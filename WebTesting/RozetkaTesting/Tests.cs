using NUnit.Framework;
using RozetkaTesting.helpers;
using RozetkaTesting.pages;

namespace RozetkaTesting
{
    [TestFixture]
    [Parallelizable]
    internal class Tests
    {
        [SetUp]
        public void OpenHomePage()
        {
            _homePage.OpenHomePage();
        }

        private HomePage _homePage;

        private const string Name = "artem";

        [OneTimeSetUp]
        public void Initialize()
        {
            LoggerHelper.InitLogger();
            
            DriverHolder.Initialize();
            _homePage = new HomePage(DriverHolder.GetDriver());
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            DriverHolder.GetDriver().Quit();
        }

        [Test]
        [Order(3)]
        public void CleanOrders()
        {
            var checkoutsForm = _homePage.GoToCheckoutsForm();
            checkoutsForm.DeleteItems();
            Assert.IsTrue(DriverHolder.GetDriver().PageSource.Contains("пуста"), "No such elem");
            LoggerHelper.Log.Info("Checkouts were cleaned");
        }

        [Test]
        [Order(1)]
        public void Login()
        {
            var loginForm = _homePage.GoToLoginForm();
            loginForm.DoLogin("zanpolbelimondo@gmail.com", "Q1w2e3");
            WaitHelper.CustomWaitNotContains(loginForm.AfterLogin, "войдите в личный");
            Assert.IsTrue(DriverHolder.GetDriver().PageSource.Contains(Name), "No such elem");
            LoggerHelper.Log.Info("Login succeed");
        }

        [Test]
        [Order(2)]
        public void OrderProduct()
        {
            var searchPage = _homePage.GoToSearchField();
            searchPage.SearchProduct("Воздухоочиститель")
                .FilterProductsByPriceAscending()
                .ChooseRandomElementFromPage();
            Assert.IsTrue(DriverHolder.GetDriver().PageSource.Contains("корзин"), "No such elem");
            LoggerHelper.Log.Info("Order is checked");
        }
    }
}