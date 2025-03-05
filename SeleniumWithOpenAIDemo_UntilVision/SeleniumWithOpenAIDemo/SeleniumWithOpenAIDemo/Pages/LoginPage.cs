using OpenQA.Selenium;

namespace SeleniumWithOpenAIDemo.Pages;

public class LoginPage(IWebDriver driver)
{
    private IWebElement UserNameField => driver.FindElement(By.Id("UserName"));
    private IWebElement PasswordField => driver.FindElement(By.Id("Password"));
    private IWebElement RememberMeCheckbox => driver.FindElement(By.Id("RememberMe"));
    private IWebElement LoginButton => driver.FindElement(By.CssSelector("input[type='submit']"));
    
    public void Login(string userName, string password)
    {
       UserNameField.SendKeys(userName);
       PasswordField.SendKeys(password);
       LoginButton.Click();
    }
}