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
    private readonly RequestUserContext _requestUserContext;

    public BusyBodyController(GetAllContracts getAllContracts, 
                              GenerateRandomLeague generateRandomLeague,
                              GetAllTeams getAllTeams,
                              GetOwner getOwner,
                              GetLeagueInfo getLeagueInfo,
                              RequestUserContext requestUserContext)
    {
        _getAllContracts = getAllContracts;
        _generateRandomLeague = generateRandomLeague;
        _getAllTeams = getAllTeams;
        _getOwner = getOwner;
        _getLeagueInfo = getLeagueInfo;
        _requestUserContext = requestUserContext;
    }

    [HttpGet]
    [Route("owner/")]
    public async Task<ActionResult<OwnerDto?>> GetOwner()
    {        
        if (_requestUserContext.Owner is Owner owner)
        {
            return await _getOwner.Execute(owner.OwnerGuid);
        }
        else 
        {
            return (OwnerDto?) null;
        }
    }

    // TODO last cheating method; after this, write the middleware and make controllers
    [HttpGet]
    [Route("league")]
    public async Task<ActionResult<LeagueDto>> GetLeague()
    {
        Owner? activeOwner = _requestUserContext.Owner;
        return await _getLeagueInfo.Execute(activeOwner);
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
