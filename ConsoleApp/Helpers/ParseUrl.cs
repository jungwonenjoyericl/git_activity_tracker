namespace Dialogue.PARSE;

// parse url
public static class ParseURL
{
    // return parsed url
    public static string Parse(string url, string name)
    {
        List<string> urlElements = new url.Split("/");
        urlElements.Insert(urlElements.Count - 2, name);        
        string parsedUrl = string.Join("/", urlElements);
        return parsedUrl;
    }
}

