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
string testUrl = Environment.GetEnvironmentVariable("PRIVATE_URL");

// top level class: default on <internal>

Console.Clear();
Console.WriteLine("\x1b[3J");

// setup api client
using var client = new HttpClient(); // create client , remove after last line of code
var gitHubService = new GitHubService(client, token); // modify client
gitHubService.InitializeClient();

// use client -- make requests
// var repos = new Repos(); // get repos
// Task<string> repoJson = repos.GetJsonDoc(service.Client);


var dialogue = new Dialogue();
dialogue.Interaction();




// TODO: move requests to another module
// string body = await resp.Content.ReadAsStringAsync();
// using var doc = JsonDocument.Parse(body);

// output class call ---- dummy arguments
// Tests
var action = dialogue.choice;
var userName = dialogue.userName;
// output object
switch (action)
{
    case "1":
    // get repo document --> output module
    var repos = new Repos(gitHubService); // get repos
    JsonDocument repoJson = await repos.GetJsonDoc(testUrl);  // for "1" as choice
    var output = new Output(name: userName, choice: action, repoDoc: repoJson);
    output.PrintOut();
    break;

    case "2":
    // Events.cs

    case "3":
    break;
    // Repos.cs
    // Events.cs
}


// var output = new Output(dialogue.userName, action, doc);


