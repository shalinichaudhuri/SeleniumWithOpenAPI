using OpenQA.Selenium;

namespace SeleniumWithOpenAIDemo.Pages;

public class HomePage(IWebDriver driver)
{
    IWebElement LoginLink => driver.FindElement(By.Id("loginLink"));
    
    IWebElement LnkEmployeeDetails => driver.FindElement(By.LinkText("Employee Details"));

    IWebElement LnkManageUsers => driver.FindElement(By.LinkText("Manage Users"));

    IWebElement LnkLogoff => driver.FindElement(By.LinkText("Log off"));
    
    
    public void ClickLogin()
    {
        LoginLink.Click();
    }
}