using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SaucedemoTests.Pages;

namespace SaucedemoTests.Tests;

[TestFixture]
public class LoginTests : PageTest
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public async Task SetUp()
    {
        _loginPage = new LoginPage(Page);
        await _loginPage.GoToAsync();
    }

    [Test]
    public async Task ValidLogin_NavigatesToInventory()
    {
        await _loginPage.LoginAsync("standard_user", "secret_sauce");
        await Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
    }

    [Test]
    public async Task LockedOutUser_ShowsErrorMessage()
    {
        await _loginPage.LoginAsync("locked_out_user", "secret_sauce");
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Sorry, this user has been locked out"));
    }

    [Test]
    public async Task EmptyUsername_ShowsErrorMessage()
    {
        await _loginPage.LoginAsync("", "secret_sauce");
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Username is required"));
    }

    [Test]
    public async Task EmptyPassword_ShowsErrorMessage()
    {
        await _loginPage.LoginAsync("standard_user", "");
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Password is required"));
    }

    [Test]
    public async Task EmptyCredentials_ShowsErrorMessage()
    {
        await _loginPage.LoginAsync("", "");
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Username is required"));
    }
}