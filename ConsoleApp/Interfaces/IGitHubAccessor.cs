using System.Text.Json;

public interface IGitHubAccessor
{
    Task<JsonDocument> GetJsonDoc(string url);
}