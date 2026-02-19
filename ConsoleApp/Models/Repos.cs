using System.Text.Json;
using ConsoleApp.Services;

namespace ConsoleApp.Models;

public class Repos(GitHubService service) : IGitHubAccessor
{
    public Task<JsonDocument> GetJsonDoc(string repoUrl) => service.GetJsonDoc(repoUrl);
}
