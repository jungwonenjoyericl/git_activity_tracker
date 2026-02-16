
// TODO: implement interface --- http req getter


namespace ConsoleApp.Models;

// TODO: get json Document  --- edit this dummy code
internal class Events
{
    private string repoUrl;
    private string activityUrl;

    private string Name = "Your";
    private int Choice;
    private JsonElement root;

    // constructor
    public Events()
    {
    }

    public void getJsonDoc()
    {
        // logic that calls what to print depending on object instances
        switch(choice)
        {
            case "1":
            string repoUrl = Environment.GetEnvironmentVariable("PRIVATE_URL");


            PrintRepos();
            return;

            case "2":
            userName = "Your";
            repoUrl = Environment.GetEnvironmentVariable("MY_URL");
            //split repourl and insert name 
            activityUrl = Environment.GetEnvironmentVariable("MY_EVENTS_URL");
            // split activity and inser name

            PrintActivities();
            return;

            case "3":
            Console.WriteLine("Username: ");
            userName = Console.ReadLine();
            var repoUrlTemplate = Environment.GetEnvironmentVariable("USER_URL");
            repoUrl = ParseUrl.Parse(repoUrlTemplate, userName);
            var activityUrlTemplate = Environment.GetEnvironmentVariable("USER_EVENTS_URL");
            activityUrl = ParseUrl.Parse(activityUrlTemplate, userName);

            PrintRepos();
            PrintActivities();
            return;        
        }
    }
}