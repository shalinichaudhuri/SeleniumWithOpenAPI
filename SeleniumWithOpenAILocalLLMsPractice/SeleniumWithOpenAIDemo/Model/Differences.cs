namespace SeleniumWithOpenAIDemo.Model;

public class Differences
{
    public Differences()
    {
        Elements = new List<Element>();
    }

    public List<Element> Elements { get; set; }
}

public class Element
{
    public Element()
    {
        FirstImage = new List<string>();
        SecondImage = new List<string>();
    }

    public string Name { get; set; }
    public List<string> FirstImage { get; set; }
    public List<string> SecondImage { get; set; }
}