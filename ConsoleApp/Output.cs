using System.Text.Json;

namespace ConsoleApp.Output;

internal static class Output
{

    private string repoUrl;
    private string activityUrl;

    private string Name = "Your";
    //private int Choice;
    private JsonElement repoRoot;

    // constructor
    // public Output(string name, string choice, JsonDocument document)
    // {
    //     if (name != null) { Name = $"{name}s"; };
    //     Choice = choice;
    //     root = document.RootElement;
    // }

    public void PrintOut(string? Name, string Choice, JsonDocument repoDoc = null, JsonDocument eventsDoc = null)
    {
        if (name != null) { this.Name = $"{Name}s"; };

        // logic that calls what to print depending on object instances
        switch(Choice)
        {
            case "1":
            repoDoc = repoDoc.RootElement;
            PrintRepos(repoDoc);
            return;

            case "2":
            eventsDoc.RootElement;
            PrintActivities(eventsDoc);
            return;

            case "3":
            repoDoc.RootElement;
            eventsDoc.RootElement;
            PrintRepos(repoDoc);
            PrintActivities(eventsDoc);
            return;        
        }
    }

    public void PrintRepos(JsonDocument doc)// do this if repo requested
    {
        Console.WriteLine($"{Name} Repositories: ");   
        Console.WriteLine("---------------");

        foreach (var repo in root.EnumerateArray())
        {
            string repoName = repo.GetProperty("name").GetString();
            Console.WriteLine($"* {repoName}");
        }

    }

    public void PrintActivities(JsonDocument doc)
    {
        Console.WriteLine($"{dialogue.userName}s Recent Activities: ");
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

            for (int i = 0; i < root.GetArrayLength(); i++ ) 
            {
                JsonElement ev = root[i];

                string eventType = ev.GetProperty("type").GetString();
                string repo = ev.GetProperty("repo").GetProperty("name").GetString();
                string time = ev.GetProperty("created_at").GetString();

                Console.WriteLine($"{time} | {eventType} | {repo}");
            }
        // }
    }
}
