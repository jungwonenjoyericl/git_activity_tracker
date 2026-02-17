using System.Text.Json;
using ConsoleApp.Services;

namespace ConsoleApp.Models;

internal class Repos(GitHubService service) : IGitHubAccessor
{
    public Task<JsonDocument> GetJsonDoc(string repoUrl) => service.GetJsonDoc(repoUrl);
}
