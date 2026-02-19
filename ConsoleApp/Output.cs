using System.Text.Json;

namespace ConsoleApp.Output;

internal class Output
{
    private readonly string name = "Your";
    private readonly JsonElement repoRoot;
    private readonly JsonElement eventsRoot;

    // Initializes the output renderer with optional repository and event payloads.
    public Output(string? name, JsonDocument repoDoc, JsonDocument eventsDoc = null)
    {
        if (name != null) { this.name = $"{name}s"; }
        repoRoot = repoDoc?.RootElement ?? default;
        eventsRoot = eventsDoc?.RootElement ?? default;
    }

    // Prints only sections that have data.
    public void PrintOut()
    {
        if (repoRoot.ValueKind != JsonValueKind.Undefined) { PrintRepos(); }
        if (eventsRoot.ValueKind != JsonValueKind.Undefined) { PrintActivities(); }
    }

    public void PrintRepos()
    {
        Console.WriteLine("---------------");
        Console.WriteLine($"{name} Repositories: ");
        Console.WriteLine("---------------");

        foreach (var repo in repoRoot.EnumerateArray())
        {
            string repoName = repo.GetProperty("name").GetString();
            Console.WriteLine($"* {repoName}");
        }
    }

    public void PrintActivities()
    {
        Console.WriteLine("---------------");
        Console.WriteLine($"{name} Recent Activities: ");
        Console.WriteLine("---------------");

        for (int i = 0; i < eventsRoot.GetArrayLength(); i++)
        {
            JsonElement ev = eventsRoot[i];

            string eventType = ev.GetProperty("type").GetString();
            string repo = ev.GetProperty("repo").GetProperty("name").GetString();
            string time = ev.GetProperty("created_at").GetString();

            Console.WriteLine($"{time} | {eventType} | {repo}");
        }
    }
}
