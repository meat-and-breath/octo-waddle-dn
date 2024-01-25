namespace Controllers;

public struct AuthRequest
{
    
    // TODO figure out how to get .NET to deserialize these from reasonable JSON (e.g., not initial caps)

    public AuthRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; init; }
    public string Password { get; init; }
}
