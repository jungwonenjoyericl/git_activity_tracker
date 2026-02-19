using System.Net.Http;
using System.Text.Json;
using DotNetEnv;

using ConsoleApp.UI;
using ConsoleApp.Output;
using ConsoleApp.Services;

// Load runtime configuration from .env.
Env.Load("../.env");

string token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

Console.Clear();
Console.WriteLine("\x1b[3J");

// Create and configure the GitHub API client.
using var client = new HttpClient();
var gitHubService = new GitHubService(client, token);
gitHubService.InitializeClient();

// Collect the user's requested operation and target URLs.
var dialogue = new Dialogue();
var request = dialogue.Interaction();

JsonDocument? repoDoc = null;
JsonDocument? eventsDoc = null;

// Fetch only the resources required by the selected menu option.
if (request.RepoUrl != null)
{
    repoDoc = await gitHubService.GetJsonDoc(request.RepoUrl);
}

if (request.ActivityUrl != null)
{
    eventsDoc = await gitHubService.GetJsonDoc(request.ActivityUrl);
}

// Render output based on whichever payloads were fetched.
var output = new Output(request.UserName, repoDoc, eventsDoc);
output.PrintOut();
