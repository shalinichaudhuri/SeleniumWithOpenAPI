using OpenAI.Chat;

namespace SeleniumWithOpenAIDemo.Utilities;

public class ForOpenAI
{
    private static readonly string apiKey = "YOUR_OPENAI_API_KEY";

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
        ChatClient client = new("gpt-4o-mini", apiKey);

        var chatMessage =
            $"Verify if locators from this Selenium POM class: {pomFileContent} match this page source: {htmlPageSource}\", only return True or False result";

        ChatCompletion completion = await client.CompleteChatAsync(chatMessage);

        return completion.Content.FirstOrDefault().Text;
    }


    public static async Task<string> PassImageToAiAsync(byte[] beforeState, byte[] afterState)
    {
        var schema = """
                     {"Elements":[{"Name":"","FirstImage":[],"SecondImage":[]},{"Name":"","FirstImage":[],"SecondImage":[]}]}
                     """;

        ChatClient client = new("gpt-4o-mini", apiKey);

        List<ChatMessage> chatMessages =
        [
            new UserChatMessage(
                ChatMessageContentPart.CreateTextPart(
                    $"Compare two images and get ONLY the differences in both as JSON output, use the following JSON schema {schema}"),
                ChatMessageContentPart.CreateImagePart(new BinaryData(beforeState), "image/png",
                    ChatImageDetailLevel.Auto),
                ChatMessageContentPart.CreateImagePart(new BinaryData(afterState), "image/png",
                    ChatImageDetailLevel.Auto)
            )
        ];

        ChatCompletion completion = await client.CompleteChatAsync(chatMessages);
        return completion.Content.FirstOrDefault().Text;
    }
}