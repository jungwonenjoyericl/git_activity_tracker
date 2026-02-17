using System;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using DotNetEnv;
using System.Net.Quic;
using System.Diagnostics.Tracing;

// import modules
using ConsoleApp.UI;
using ConsoleApp.Output;
using ConsoleApp.Services;
using ConsoleApp.Models;

Env.Load("../.env");
string token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

// top level class: default on <internal>

Console.Clear();
Console.WriteLine("\x1b[3J");

// setup api client
using var client = new HttpClient();
var service = new GithubService(client);
service.InitializeClient();

var dialogue = new Dialogue();

// TODO: module with infogetter using the created client
string repoUrl = dialogue.repoUrl;
var resp = await client.GetAsync(repoUrl);
int statusCode = (int)resp.StatusCode;

if (statusCode != 200) 
{
    throw new ArgumentException ($"Could not connect to Github API: statuscode: {statusCode}", nameof(statusCode));
};

Console.WriteLine("---------------");
Console.WriteLine($"Status: {(int)resp.StatusCode} {resp.ReasonPhrase}");
Console.WriteLine("---------------");



// TODO: move requests to another module
string body = await resp.Content.ReadAsStringAsync();
using var doc = JsonDocument.Parse(body);

// output class call ---- dummy arguments
// Tests
private readonly string action = dialogue.choice;
private readonly string userName = dialogue.userName;
var output = new Output();
switch (action)
{
    case "1":
    // get repo document --> output module
    var repos = new Repos();
    output.PrintOut(Name: userName, Choice: action, repoDoc: repos);
    break;

    case "2":
    // Events.cs

    case "3":
    // Repos.cs
    // Events.cs
}


// var output = new Output(dialogue.userName, action, doc);

if (dialogue.choice == "2") { goto exit_to_activity_getter; }


Console.WriteLine($"{dialogue.userName}s Recent Activities: ");
Console.WriteLine("---------------");


