using System.Text.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumWithOpenAIDemo.Model;
using SeleniumWithOpenAIDemo.Pages;
using SeleniumWithOpenAIDemo.Utilities;
using Xunit.Abstractions;

namespace SeleniumWithOpenAIDemo;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task Test1()
    {
        ChromeDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://eaapp.somee.com");
        
        HomePage homePage = new HomePage(driver);
        var beforeState = driver.GetScreenshot().AsByteArray;
        homePage.ClickLogin();
        
        LoginPage loginPage = new LoginPage(driver);
        
        var loginPagePOM = ForOpenAI.ReadPageObjectModelPage("LoginPage.cs");
        var openAIResponse = await ForOpenAI.VerifyPageLocatorFromAiAsync(loginPagePOM, driver.PageSource);

        if (openAIResponse.Contains("True"))
        {
            loginPage.Login("admin", "password");
            // Added explicit wait for the page to load completely before taking the screenshot
            Thread.Sleep(2000);
            var afterState = driver.GetScreenshot().AsByteArray;

            var message = await ForOpenAI.PassImageToAiAsync(beforeState, afterState);
            var responseData =
                JsonSerializer.Deserialize<Differences>(message.Replace("```json", "").Replace("```", ""));

            foreach (var element in responseData.Elements)
            {
                _testOutputHelper.WriteLine(
                    $"Differences are in: {element.Name} with First Image: {string.Join(", ", element.FirstImage)} and Second Image: {string.Join(", ", element.SecondImage)}");
            }
        }
        else
            Assert.Fail("Locators not found/not matching in the page source");
                    
        driver.Quit();
    }
}