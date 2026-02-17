using System.Text.Json;
using ConsoleApp.Services;

namespace ConsoleApp.Models;

internal class Events(GitHubService service) : IGitHubAccessor
{
    public Task<JsonDocument> GetJsonDoc(string eventsUrl) => service.GetJsonDoc(eventsUrl);
}
