using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SaucedemoTests.Pages;

namespace SaucedemoTests.Tests;

[TestFixture]
public class LoginTests : TestBase
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public void SetUpLoginPage()
    {
        _loginPage = new LoginPage(Page);
    }

    [Test]
    public async Task ValidLogin_NavigatesToInventory()
    {
        var user = Settings.Users["StandardUser"];
        await Page.FillAsync("#user-name", user.Username);
        await Page.FillAsync("#password", user.Password);
        await Page.ClickAsync("#login-button");
        await Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
    }

    [Test]
    public async Task LockedOutUser_ShowsErrorMessage()
    {
        var user = Settings.Users["LockedOutUser"];
        await Page.FillAsync("#user-name", user.Username);
        await Page.FillAsync("#password", user.Password);
        await Page.ClickAsync("#login-button");
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Sorry, this user has been locked out"));
    }

    [Test]
    public async Task EmptyUsername_ShowsErrorMessage()
    {
        await Page.FillAsync("#user-name", "");
        await Page.FillAsync("#password", "password");
        await Page.ClickAsync("#login-button");
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Username is required"));
    }

    [Test]
    public async Task EmptyPassword_ShowsErrorMessage()
    {
        await Page.FillAsync("#user-name", "standard_user");
        await Page.FillAsync("#password", "");
        await Page.ClickAsync("#login-button");
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Password is required"));
    }

    [Test]
    public async Task EmptyCredentials_ShowsErrorMessage()
    {
        await Page.FillAsync("#user-name", "");
        await Page.FillAsync("#password", "");
        await Page.ClickAsync("#login-button");
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Username is required"));
    }
}