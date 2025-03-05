using OpenQA.Selenium;

namespace SeleniumWithOpenAIDemo.Pages;

public class LoginPage(IWebDriver driver)
{
    private IWebElement UserNameField => driver.FindElement(By.Id("UserName"));
    private IWebElement PasswordField => driver.FindElement(By.Id("Password"));
    private IWebElement LoginButton => driver.FindElement(By.Id("loginIn"));

    public void Login(string userName, string password)
    {
        UserNameField.SendKeys(userName);
        PasswordField.SendKeys(password);
        LoginButton.Click();
    }
}