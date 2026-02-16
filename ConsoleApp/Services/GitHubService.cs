using System.Net.Http.Headers;


namespace ConsoleApp.Services;
internal static class GitHubService(HttpClient client)
{
    // setup api client
    private readonly string token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
    public readonly HttpClient Client = client;

    // TODO: implement dependency injection --- move this into a module
    public void InitializeClient()
    {
        Client.DefaultRequestHeaders.UserAgent.ParseAdd("git-activity-tracker/1.0");
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

        if (!string.IsNullOrWhiteSpace(token))
        {
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}