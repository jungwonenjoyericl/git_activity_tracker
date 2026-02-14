namespace ConsoleApp.UI;

using ConsoleApp.Helpers;

public class Dialogue
{        
    public string repoUrl;
    public string activityUrl;
    public string userName;
    public string choice;

    public void Start()
    {
        while (true) 
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");

            Console.WriteLine("What do you wan't to do?");
            Console.WriteLine("1: Track your own repositories");
            Console.WriteLine("2: Track your recent activities(public)");
            Console.WriteLine("3: Track [username]s  public repositories + activities");
            choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                repoUrl = Environment.GetEnvironmentVariable("PRIVATE_URL");
                return;

                case "2":
                userName = "Your";
                repoUrl = Environment.GetEnvironmentVariable("MY_URL");
                //split repourl and insert name 
                activityUrl = Environment.GetEnvironmentVariable("MY_EVENTS_URL");
                // split activity and inser name
                return;

                case "3":
                Console.WriteLine("Username: ");
                userName = Console.ReadLine();
                var repoUrlTemplate = Environment.GetEnvironmentVariable("USER_URL");
                repoUrl = ParseUrl.Parse(repoUrlTemplate, userName);
                var activityUrlTemplate = Environment.GetEnvironmentVariable("USER_EVENTS_URL");
                activityUrl = ParseUrl.Parse(activityUrlTemplate, userName);
                return;        
            }
        }
    }
}