using System;
using System.Collections.Generic;


// parse url
namespace ConsoleApp.Helpers;

public static class ParseUrl

    {
    // return parsed url
    public static string Parse(string url, string name)
    {
        string[] urlElements = url.Split("/");
        urlElements[^2] = name;
        string parsedUrl = string.Join("/", urlElements);
        return parsedUrl;
    }
}


