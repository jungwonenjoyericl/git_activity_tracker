namespace ConsoleApp.UI;

using ConsoleApp.Helpers;

public class Dialogue
{
    // Handles user interaction and returns a request object for the selected action.
    public Request Interaction()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
            }
            catch (IOException)
            {
                // Expected in test runners or redirected terminals without a real console handle.
            }

            Console.WriteLine("What do you wan't to do?");
            Console.WriteLine("1: Track your own repositories");
            Console.WriteLine("2: Track your recent activities(public)");
            Console.WriteLine("3: Track [username]s  public repositories + activities");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return new Request(
                        UserName: null,
                        RepoUrl: Environment.GetEnvironmentVariable("PRIVATE_URL"),
                        ActivityUrl: null
                    );

                case "2":
                    return new Request(
                        UserName: null,
                        RepoUrl: null,
                        ActivityUrl: Environment.GetEnvironmentVariable("MY_EVENTS_URL")
                    );

                case "3":
                    Console.WriteLine("Username: ");
                    var inputName = Console.ReadLine();

                    var repoUrlTemplate = Environment.GetEnvironmentVariable("USER_URL");
                    var activityUrlTemplate = Environment.GetEnvironmentVariable("USER_EVENTS_URL");

                    // Build user-specific endpoints from configured templates.
                    return new Request(
                        UserName: inputName,
                        RepoUrl: ParseUrl.Parse(repoUrlTemplate, inputName),
                        ActivityUrl: ParseUrl.Parse(activityUrlTemplate, inputName)
                    );
            }
        }
    }
}
