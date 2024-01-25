using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("auth/")]
public class DummyAuthController : ControllerBase
{

    public class UserModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class Response
    {
        public string token { get; set; }
    }

    [HttpPost]
    [Route("token")]
    public ActionResult<Response> AuthorizeUser([FromBody] UserModel userModel)
    {
        // TODO this is supposed to be dummy auth, but maybe check that the user exists, at least?
        string[] secret = ["verysecret"];

        var token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(secret)
                    //   .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                    //   .AddClaim("userId", userModel.UserId)
                    //   .AddClaim("claim2", "claim2-value")
                    //   .Encode();
                    .Encode(userModel);

        return new Response() { token = token };
    }
}
