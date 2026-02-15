using System.Text.Json;

namespace ConsoleApp.Output;

internal static class Output
{
    // constructor
    public Output(string name = "Your", string url, JsonDocument document)
    {
        string name = name;
        string url = url;
        JsonElement root = document.RootElement;
    }

    public void PrintOut()
    {
        // logic that calls what to print depending on object instances
        if (this.name)
        {
            // print the public repos 
        }
        else
        {
            // print your all private repos
        }
    }

    private void PrintRepos()// do this if repo requested
    {
        Console.WriteLine($"{this.name}s Repositories: ");
        Console.WriteLine("---------------");

        foreach (var repo in Document.EnumerateArray())
        {
            string repoName = repo.GetProperty("name").GetString();
            Console.WriteLine($"* {repoName}");
        }

    }
}
