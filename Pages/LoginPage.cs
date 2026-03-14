using Microsoft.Playwright;

namespace SaucedemoTests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    // Selectors
    private const string UsernameInput = "[data-test='username']";
    private const string PasswordInput = "[data-test='password']";
    private const string LoginButton = "[data-test='login-button']";
    private const string ErrorMessage = "[data-test='error']";

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task GoToAsync()
    {
        await _page.GotoAsync("https://www.saucedemo.com");
    }

    public async Task LoginAsync(string username, string password)
    {
        await _page.FillAsync(UsernameInput, username);
        await _page.FillAsync(PasswordInput, password);
        await _page.ClickAsync(LoginButton);
    }

    public async Task<string> GetErrorMessageAsync()
    {
        return await _page.InnerTextAsync(ErrorMessage);
    }
}