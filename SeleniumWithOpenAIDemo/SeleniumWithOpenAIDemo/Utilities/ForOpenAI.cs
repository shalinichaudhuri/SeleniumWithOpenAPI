using OpenAI.Chat;

namespace SeleniumWithOpenAIDemo.Utilities;

public class ForOpenAI
{
    
    private static readonly string apiKey = "YOUR-OPENAI-API-KEY-HERE";

    public static string ReadPageObjectModelPage(string pageName)
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        var projectRoot = Directory.GetParent(currentDirectory)
            .Parent
            .Parent
            .FullName;
        
        var pagePath = Path.Combine(projectRoot, "Pages", pageName);
        
        var pageContent = File.ReadAllText(pagePath);
        
        return pageContent;
    }
    
    
    public static async Task<string> VerifyPageLocatorFromAiAsync(string pomFileContent, string htmlPageSource)
    {
        ChatClient client = new(model: "gpt-4o-mini", apiKey);
        
        var chatMessage = $"Verify if locators from this Selenium POM class: {pomFileContent} match this page source: {htmlPageSource}\", only return True or False result";

        ChatCompletion completion = await client.CompleteChatAsync(chatMessage);

        return completion.Content.FirstOrDefault().Text;
    }
}