namespace OctoWaddle.Domain;

public class Team
{
    public Team(TeamGuid teamGuid, OwnerGuid onwerGuid, string name, string location, DateOnly creationDate)
    {
        TeamGuid = teamGuid;
        OnwerGuid = onwerGuid;
        Name = name;
        Location = location;
        CreationDate = creationDate;
    }

    public TeamGuid TeamGuid { get; init; }

    public OwnerGuid OnwerGuid {get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public DateOnly CreationDate { get; init; }

    
}