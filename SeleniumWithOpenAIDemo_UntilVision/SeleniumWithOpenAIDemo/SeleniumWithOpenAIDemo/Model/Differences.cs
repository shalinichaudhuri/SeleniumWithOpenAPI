namespace SeleniumWithOpenAIDemo.Model;

public class Differences
{
    public List<Element> Elements { get; set; }

    public Differences()
    {
        Elements = new List<Element>();
    }
}

public class Element
{
    public string Name { get; set; }
    public List<string> FirstImage { get; set; }
    public List<string> SecondImage { get; set; }

    public Element()
    {
        FirstImage = new List<string>();
        SecondImage = new List<string>();
    }
}