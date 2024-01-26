using JWT;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using OctoWaddle;
using OctoWaddle.Domain.Entities;
using OctoWaddle.UseCases;

namespace Controllers;

[ApiController]
[Route("busybody/")]
public class BusyBodyController : ControllerBase
{
    // TODO this feels gross; is there a better pattern? [ed. distinct controllers]
    private readonly GetAllContracts _getAllContracts;
    private readonly GenerateRandomLeague _generateRandomLeague;
    private readonly GetAllTeams _getAllTeams;
    private readonly GetOwner _getOwner;
    private readonly GetLeagueInfo _getLeagueInfo;

    public BusyBodyController(GetAllContracts getAllContracts, 
                              GenerateRandomLeague generateRandomLeague,
                              GetAllTeams getAllTeams,
                              GetOwner getOwner,
                              GetLeagueInfo getLeagueInfo)
    {
        _getAllContracts = getAllContracts;
        _generateRandomLeague = generateRandomLeague;
        _getAllTeams = getAllTeams;
        _getOwner = getOwner;
        _getLeagueInfo = getLeagueInfo;
    }

    [HttpGet]
    [Route("owner/")]
    public async Task<ActionResult<OwnerDto>> GetOwner()
    {
        // TODO make a middleware (?) that gets the owner from the JWT for all callers
        var authorizationHeader = HttpContext.Request.Headers["Authorization"];
        
        string token = string.Empty;
        if (authorizationHeader.ToString().StartsWith("Bearer"))
        {
            token = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
        }

        string[] secret = ["verysecret"]; // because it is

        var payload = JwtBuilder.Create()
                                .WithAlgorithm(new HMACSHA256Algorithm())
                                .WithSecret(secret)
                                .Decode<UserAuthClaim>(token);
        var owners = await _getOwner.GatAllOwners();
        var owner = owners.Find(o => payload.UserId.Equals(o.OwnerGuid));

        Console.WriteLine($"Looking up {payload.UserId}");
        if (false /*owner is not null*/)
        {
            Console.WriteLine($"Found Owner {owner.Name}");
        }
        else
        {
            Console.WriteLine("Not found in Owners");
            foreach (var o in owners)
            {

            }
        }

        return owner;
    }

    // TODO last cheating method; after this, write the middleware and make controllers
    [HttpGet]
    [Route("league")]
    public async Task<ActionResult<LeagueDto>> GetLeague()
    {
        return await _getLeagueInfo.Execute(null);
    }

    [HttpGet]
    [Route("contract/all")]
    public async Task<ActionResult<List<Contract>>> GetAllContracts()
    {
        // TODO this lacks a mapping from domain to API contracts [as does everything]

        return await _getAllContracts.Execute();
    }

    [HttpGet]
    [Route("team/all")]
    public async Task<ActionResult<List<Team>>> GetAllTeams()
    {
        return await _getAllTeams.Execute();
    }

    [HttpPost]
    [Route("league/generate")]
    public async void GenerateLeague()
    {
        await _generateRandomLeague.Execute();
    }

}
