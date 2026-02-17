using System.Text.Json;

namespace ConsoleApp.Output;

internal class Output
{
    private readonly string Choice;
    private readonly string Name = "Your";
    //private int Choice;
    private readonly JsonElement repoRoot;
    private readonly JsonElement eventsRoot;

    // constructor
    public Output(string? name, string choice, JsonDocument repoDoc = null, JsonDocument eventsDoc = null)
    {
        repoDoc ??= JsonDocument.Parse("{}"); 
        eventsDoc ??= JsonDocument.Parse("{}"); 

        if (name != null) { Name = $"{name}s"; };  
        Choice = choice;
        repoRoot = repoDoc.RootElement;
        eventsRoot = eventsDoc.RootElement;
    }

    public void PrintOut()
    {

        // logic that calls what to print depending on object instances
        switch(Choice)
        {
            case "1":
            PrintRepos();
            return;

            case "2":
            PrintActivities();
            return;

            case "3":
            PrintRepos();
            PrintActivities();
            return;        
        }
    }

    public void PrintRepos()// do this if repo requested
    {
        Console.WriteLine($"{Name} Repositories: ");   
        Console.WriteLine("---------------");

        foreach (var repo in repoRoot.EnumerateArray())
        {
            string repoName = repo.GetProperty("name").GetString();
            Console.WriteLine($"* {repoName}");
        }

    }

    public void PrintActivities()
    {
        Console.WriteLine($"{Name} Recent Activities: ");
        Console.WriteLine("---------------");

        // if (Choice != null)
        // {
            // if (choice == "3")
            // {
            //     url = $"https://api.github.com/users/{dialogue.userName}/events"; 
            // } 
            // else
            // {
            //     url = Environment.GetEnvironmentVariable("MY_EVENTS_URL"); 
            // }

            // // TODO: move request to module
            // var resp2 = await client.GetAsync(dialogue.activityUrl);
            // string body2 = await resp2.Content.ReadAsStringAsync();
            // using var doc2 = JsonDocument.Parse(body2);
            // JsonElement root = doc2.RootElement;

        for (int i = 0; i < eventsRoot.GetArrayLength(); i++ ) 
        {
            JsonElement ev = eventsRoot[i];

            string eventType = ev.GetProperty("type").GetString();
            string repo = ev.GetProperty("repo").GetProperty("name").GetString();
            string time = ev.GetProperty("created_at").GetString();

            Console.WriteLine($"{time} | {eventType} | {repo}");
        }
        // }
    }
}
