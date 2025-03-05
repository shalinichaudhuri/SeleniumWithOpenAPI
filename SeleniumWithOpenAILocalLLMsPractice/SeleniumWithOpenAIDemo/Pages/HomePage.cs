using OpenQA.Selenium;

namespace SeleniumWithOpenAIDemo.Pages;

public class HomePage(IWebDriver driver)
{
    private IWebElement LoginLink => driver.FindElement(By.Id("loginLink"));

    private IWebElement LnkEmployeeDetails => driver.FindElement(By.LinkText("Employee Details"));

    private IWebElement LnkManageUsers => driver.FindElement(By.LinkText("Manage Users"));

    private IWebElement LnkLogoff => driver.FindElement(By.LinkText("Log off"));


    public void ClickLogin()
    {
        LoginLink.Click();
    }
}