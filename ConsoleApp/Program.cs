using System;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using DotNetEnv;

Env.Load("../.env");

string url = Environment.GetEnvironmentVariable("USER_URL");
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

// test: input -> get input repos
string userName = null;
while (true) {
    Console.WriteLine("List all my repos? Y/N");
    string choice = Console.ReadLine();
    if (choice == "N")
    {
        Console.WriteLine("Username: ");
        userName = Console.ReadLine();
        url = "https://api.github.com/users/" + userName + "/repos";
        break;
    } 
    else
    {
        break;
    }
}

var resp = await client.GetAsync(url);
Console.WriteLine($"Status: {(int)resp.StatusCode} {resp.ReasonPhrase}");
Console.WriteLine("---------------");
string body = await resp.Content.ReadAsStringAsync();
using var doc = JsonDocument.Parse(body);

Console.WriteLine($"{userName}s Repositories: ");
Console.WriteLine("---------------");

foreach (var repo in doc.RootElement.EnumerateArray())
{
    string name = repo.GetProperty("name").GetString();
    Console.WriteLine($"{name}");
}
