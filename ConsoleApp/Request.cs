namespace ConsoleApp.UI;

// Immutable request contract passed from the dialogue layer to the program flow.
public sealed record Request(
    string? UserName,
    string? RepoUrl,
    string? ActivityUrl
);
