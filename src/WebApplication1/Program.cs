using Controllers;
using Infrastructure;
using OctoWaddle.Domain;
using OctoWaddle.Domain.Repositories;
using OctoWaddle.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ContractRepository, SimpleContractRepository>();
builder.Services.AddSingleton<PlayerRepository, SimplePlayerReporistory>();
builder.Services.AddSingleton<TeamRepository, SimpleTeamRepository>();
builder.Services.AddSingleton<OwnerRepository, SimpleOwnerRepository>();

builder.Services.AddScoped<RequestUserContext>();

builder.Services.AddTransient<GetAllContracts>();
builder.Services.AddTransient<GenerateRandomLeague>();
builder.Services.AddTransient<GetAllTeams>();
builder.Services.AddTransient<GetTeam>();
builder.Services.AddTransient<GetOwner>();
builder.Services.AddTransient<TradePlayer>();
builder.Services.AddTransient<GetLeagueInfo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseExtractOwner();
app.UseAuthorization();

app.MapControllers();

app.Run();
