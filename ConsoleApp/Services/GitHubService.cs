using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.VisualBasic;


namespace ConsoleApp.Services;
internal class GitHubService
{
    // setup api client
    private readonly string _token;
    private readonly HttpClient _Client;

    public GitHubService(HttpClient client, string token)
    {
        _Client = client;
        _token = token;
    }

    // TODO: implement dependency injection --- move this into a module
    public void InitializeClient()
    {
        _Client.DefaultRequestHeaders.UserAgent.ParseAdd("git-activity-tracker/1.0");
        _Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

        if (!string.IsNullOrWhiteSpace(_token))
        {
            _Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token);
        }
    }

    public Task<HttpResponseMessage> GetAsync(string url) => _Client.GetAsync(url);

    public async Task<JsonDocument> GetJsonDoc(string url)
    {
        var resp = await _Client.GetAsync(url);
        GetStatusCode(resp);

        var body = await resp.Content.ReadAsStringAsync();
        return JsonDocument.Parse(body);
    }

    public void GetStatusCode(HttpResponseMessage resp){
        int statusCode = (int)resp.StatusCode;
        if (statusCode != 200) 
        {
            throw new ArgumentException ($"Could not connect to Github API; statuscode: {statusCode}", nameof(statusCode));
        };
    }

}