using System.Text.Json;

internal interface IGitHubAccessor
{
    Task<JsonDocument> GetJsonDoc(string url);
}