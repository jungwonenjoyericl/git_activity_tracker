using System;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using DotNetEnv;
using System.Net.Quic;
using System.Diagnostics.Tracing;

// import modules
using ConsoleApp.UI;

Env.Load("../.env");

Console.Clear();
Console.WriteLine("\x1b[3J");

// setup api client
string url = null;
string token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

using var client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("git-activity-tracker/1.0");
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

if (!string.IsNullOrWhiteSpace(token))
{
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", token);
}

var dialogue = new Dialogue();
dialogue.Start();

Console.WriteLine(dialogue.repoUrl);
var resp = await client.GetAsync(dialogue.repoUrl);
int statusCode = (int)resp.StatusCode;

if (statusCode != 200) 
{
    throw new ArgumentException ($"Could not connect to Github API: statuscode: {statusCode}", nameof(statusCode));
};

Console.WriteLine("---------------");
Console.WriteLine($"Status: {(int)resp.StatusCode} {resp.ReasonPhrase}");
Console.WriteLine("---------------");
string body = await resp.Content.ReadAsStringAsync();
using var doc = JsonDocument.Parse(body);

if (dialogue.choice == "2") { goto exit_to_activity_getter; }

Console.WriteLine($"{dialogue.userName}s Repositories: ");
Console.WriteLine("---------------");

foreach (var repo in doc.RootElement.EnumerateArray())
{
    string name = repo.GetProperty("name").GetString();
    Console.WriteLine($"* {name}");
}

exit_to_activity_getter: ;
Console.WriteLine($"{dialogue.userName}s Recent Activities: ");
Console.WriteLine("---------------");


// TODO: if parseUrl returns, do this    arguments: (dialogue.activityUrl, dialogue.userName)
if (dialogue.activityUrl != null)
{
    if (dialogue.choice == "3")
    {
        url = $"https://api.github.com/users/{dialogue.userName}/events";
    } 
    else
    {
        url = Environment.GetEnvironmentVariable("MY_EVENTS_URL");
    }

    var resp2 = await client.GetAsync(dialogue.activityUrl);
    string body2 = await resp2.Content.ReadAsStringAsync();
    using var doc2 = JsonDocument.Parse(body2);
    JsonElement root = doc2.RootElement;

    for (int i = 0; i < root.GetArrayLength(); i++ ) 
    {
        JsonElement ev = root[i];

        string eventType = ev.GetProperty("type").GetString();
        string repo = ev.GetProperty("repo").GetProperty("name").GetString();
        string time = ev.GetProperty("created_at").GetString();

        Console.WriteLine($"{time} | {eventType} | {repo}");
    }
}
