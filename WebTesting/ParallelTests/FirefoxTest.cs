using NUnit.Framework;

namespace ParallelTests
{
    [TestFixture]
    [Parallelizable]
    public class FirefoxTest : Hooks
    {
        public FirefoxTest() : base(BrowserTypeEnum.Firefox)
        {
        }

        [Test]
        public void SendFirefox()
        {
            SendSomething("Firefox");
        }
    }

    [TestFixture]
    [Parallelizable]
    public class ChromeTest : Hooks
    {
        public ChromeTest() : base(BrowserTypeEnum.Chrome)
        {
        }

        [Test]
        public void SendChrome()
        {
            SendSomething("Chrome");
        }
    }
}