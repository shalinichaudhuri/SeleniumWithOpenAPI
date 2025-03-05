using HtmlAgilityPack;
using Microsoft.Extensions.AI;
using Xunit.Abstractions;

namespace SeleniumWithOpenAIDemo.Utilities;

public class LocalLLMAPIs
{
    public static async Task ChatWithLLM(string message, ITestOutputHelper outputHelper)
    {
        IChatClient ollamaClient = new OllamaChatClient(new Uri("http://localhost:11434/"), "llama3.1");
        var response = await ollamaClient.CompleteAsync(message);
        outputHelper.WriteLine(response.Message.Text);
    }


    /// <summary>
    ///     Verifies if the locators from a Selenium Page Object Model (POM) class match the given HTML page source.
    ///     The function uses an LLM (Large Language Model) API to perform the verification.
    /// </summary>
    /// <param name="pomFileContent">The content of the Selenium POM class as a string.</param>
    /// <param name="htmlPageSource">The HTML page source as a string.</param>
    /// <returns>
    ///     A string containing the LLM's response. The response will be either "True" or "False", indicating whether the
    ///     locators match the page source.
    /// </returns>
    public static async Task<string> VerifyPageLocatorFromAiAsync(string pomFileContent, string htmlPageSource)
    {
        IChatClient ollamaClient = new OllamaChatClient(new Uri("http://localhost:11434/"), "llama3.1");

        var chatMessage =
            $"Verify if locators from this Selenium POM class: {pomFileContent} match this page source: {htmlPageSource}\", only return True or False result, Dont give me any Explanations.";

        var response = await ollamaClient.CompleteAsync(chatMessage);

        return response.Message.Text;
    }

    /// <summary>
    ///     Extracts the inner HTML of the first form element from the given HTML page source.
    /// </summary>
    /// <param name="pageSource">The HTML page source as a string.</param>
    /// <returns>
    ///     The inner HTML of the first form element if found, otherwise null.
    /// </returns>
    public static string? ExtractPageSource(string pageSource)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(pageSource);
        var nodes = doc.DocumentNode.SelectSingleNode("//form");
        return nodes?.InnerHtml;
    }
}