using ConsoleApp.Helpers;
using ConsoleApp.UI;
using Xunit;
using DotNetEnv;


namespace ConsoleApp.Tests;


// Test
public class UnitTest1
{
    [Fact]
    public void Parse_Replaces_Username_In_Repo_Url()
    {
        var template = "https://api.github.com/users/placeholder/repos";
        var result = ParseUrl.Parse(template, "jungwon");

        Assert.Equal("https://api.github.com/users/jungwon/repos", result);
    }

    [Fact]
    public void Parse_Replaces_Username_In_Events_Url()
    {
        var template = "https://api.github.com/users/placeholder/events";
        var result = ParseUrl.Parse(template, "jungwon");

        Assert.Equal("https://api.github.com/users/jungwon/events", result);
    }

    [Fact]
    public void Interaction_Choice1_Returns_Private_Repo_Request()
    {
        var originalIn = Console.In;
        Console.SetIn(new StringReader("1" + Environment.NewLine));

        try
        {
            var dialogue = new Dialogue();

            var result = dialogue.Interaction();

            Assert.Equal(Environment.GetEnvironmentVariable("PRIVATE_URL"), result.RepoUrl);
            Assert.Null(result.ActivityUrl);
        }
        finally
        {
            Console.SetIn(originalIn);
        }
    }

    [Fact]
    public void Interaction_Choice2_Returns_Public_Events_Request()
    {
        var originalIn = Console.In;
        Console.SetIn(new StringReader("2" + Environment.NewLine));

        try
        {
            var dialogue = new Dialogue();

            var result = dialogue.Interaction();

            Assert.Equal(Environment.GetEnvironmentVariable("MY_EVENTS_URL"), result.ActivityUrl);
            Assert.Null(result.RepoUrl);
        }
        finally
        {
            Console.SetIn(originalIn);
        }
    }

     [Fact]
    public void Interaction_Choice3_Returns_Public_Repo_And_Events_Request()
    {
        var originalIn = Console.In;            
        var username = "jungwonenjoyericl";

        Console.SetIn(new StringReader($"3{Environment.NewLine}{username}{Environment.NewLine}"));


        try
        {
            var dialogue = new Dialogue();

            Environment.SetEnvironmentVariable("USER_URL", "https://api.github.com/users/placeholder/repos");
            Environment.SetEnvironmentVariable("USER_EVENTS_URL", "https://api.github.com/users/placeholder/events");

            var result = dialogue.Interaction();


            var repoTemplate = "https://api.github.com/users/placeholder/repos";            
            var eventsTemplate = "https://api.github.com/users/placeholder/events";

            var expectedRepoUrl = ParseUrl.Parse(repoTemplate, username);
            var expectedActivityUrl = ParseUrl.Parse(eventsTemplate, username);

            Assert.Equal(username, result.UserName);   
            Assert.Equal(expectedRepoUrl, result.RepoUrl);
            Assert.Equal(expectedActivityUrl, result.ActivityUrl);
        }
        finally
        {
            Console.SetIn(originalIn);
        }
    }
}
