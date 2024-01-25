namespace Controllers;

public struct AuthResponse
{
    public AuthResponse(string access_token)
    {
        this.acess_token = access_token;
    }

    public string acess_token { get; set; }
    public long expires_in { get; init; } = 3600;
    public string token_type { get; init; } = "Bearer";
}