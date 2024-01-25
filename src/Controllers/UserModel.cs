namespace Controllers;

public class UserAuthClaim
{


    public UserAuthClaim(string userId, long expiration)
    {
        UserId = userId;
        Expiration = expiration;
    }

    public string UserId { get; init; }
    public long Expiration { get; init; }
}