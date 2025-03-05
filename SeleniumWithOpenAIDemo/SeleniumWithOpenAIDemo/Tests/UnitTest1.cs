using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumWithOpenAIDemo.Pages;
using SeleniumWithOpenAIDemo.Utilities;

namespace SeleniumWithOpenAIDemo;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://eaapp.somee.com");
        
        HomePage homePage = new HomePage(driver);
        homePage.ClickLogin();
        
        LoginPage loginPage = new LoginPage(driver);
        
        var loginPagePOM = ForOpenAI.ReadPageObjectModelPage("LoginPage.cs");
        var openAIResponse = await ForOpenAI.VerifyPageLocatorFromAiAsync(loginPagePOM, driver.PageSource);
        
        if(openAIResponse.Contains("True"))
            loginPage.Login("admin", "password");
        else
            Assert.Fail("Locators not found/not matching in the page source");
    }
}