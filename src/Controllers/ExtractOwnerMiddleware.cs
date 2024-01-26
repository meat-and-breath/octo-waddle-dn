using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OctoWaddle;
using OctoWaddle.Domain.Entities;
using OctoWaddle.UseCases;

namespace Controllers;

public class ExtractOwnerMiddleware
{
    private readonly RequestDelegate _next;

    public ExtractOwnerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, GetOwner getOwner, RequestUserContext requestUserContext)
    {
        var authorizationHeader = context.Request.Headers["Authorization"];
        
        if (authorizationHeader.ToString().StartsWith("Bearer"))
        {
            string token = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
            string[] secret = ["verysecret"]; // because it is

            var payload = JwtBuilder.Create()
                                    .WithAlgorithm(new HMACSHA256Algorithm())
                                    .WithSecret(secret)
                                    .Decode<UserAuthClaim>(token);

            var ownerGuid = new OwnerGuid(payload.UserId);
            var ownerMaybe = await getOwner.Execute(ownerGuid);

            Console.WriteLine($"Looking up {payload.UserId}");
            if (ownerMaybe is OwnerDto owner)
            {
                Console.WriteLine($"Found Owner {owner.Name}");
                requestUserContext.Owner = owner.GetOwner();
            }
            else
            {
                Console.WriteLine("Not found in Owners");
            }
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}

public static class ExtractOwnerMiddlewareExtensions
{
    public static IApplicationBuilder UseExtractOwner(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExtractOwnerMiddleware>();
    }
}