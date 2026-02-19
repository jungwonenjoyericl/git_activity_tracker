using System.Net.Http.Headers;
using System.Text.Json;

namespace ConsoleApp.Services;

public class GitHubService
{
    private readonly string _token;
    private readonly HttpClient _client;

    public GitHubService(HttpClient client, string token)
    {
        _client = client;
        _token = token;
    }

    // Applies required headers for GitHub REST API requests.
    public void InitializeClient()
    {
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("git-activity-tracker/1.0");
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

        if (!string.IsNullOrWhiteSpace(_token))
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token);
        }
    }

    // Executes a request and parses the JSON response body.
    public async Task<JsonDocument> GetJsonDoc(string url)
    {
        var resp = await _client.GetAsync(url);
        GetStatusCode(resp);

        var body = await resp.Content.ReadAsStringAsync();
        return JsonDocument.Parse(body);
    }

    // Enforces current behavior: only HTTP 200 is accepted.
    public void GetStatusCode(HttpResponseMessage resp)
    {
        int statusCode = (int)resp.StatusCode;
        if (statusCode != 200)
        {
            throw new ArgumentException($"Could not connect to Github API; statuscode: {statusCode}", nameof(statusCode));
        }
    }
}
