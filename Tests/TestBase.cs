using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SaucedemoTests.Config;

namespace SaucedemoTests.Tests
{
    public class TestBase : PageTest
    {
        protected TestSettings Settings;

        [SetUp]
        public async Task SetUp()
        {
            Settings = ConfigHelper.Settings;
            await Page.GotoAsync(Settings.BaseUrl);
        }
    }
}