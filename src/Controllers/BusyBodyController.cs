﻿using Microsoft.AspNetCore.Mvc;
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

    public BusyBodyController(GetAllContracts getAllContracts, 
                              GenerateRandomLeague generateRandomLeague,
                              GetAllTeams getAllTeams)
    {
        _getAllContracts = getAllContracts;
        _generateRandomLeague = generateRandomLeague;
        _getAllTeams = getAllTeams;
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