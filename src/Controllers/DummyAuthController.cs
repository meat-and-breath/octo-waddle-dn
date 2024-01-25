using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Controllers;

[ApiController]
[Route("auth/")]
public partial class DummyAuthController : ControllerBase
{

    [HttpPost]
    [Route("token")]
    public ActionResult<AuthResponse> AuthorizeUser([FromBody] AuthRequest authRequest)
    {
        // TODO this is supposed to be dummy auth, but maybe check that the user exists, at least?
        string[] secret = ["verysecret"];

        if (! authRequest.Password.Equals("password")) // is most secure
        {
            return Unauthorized();
        }

        var expiration = DateTimeOffset.Now.AddSeconds(3600).ToUnixTimeMilliseconds();
        UserAuthClaim userAuthClaim = new (authRequest.UserName, expiration);

        var token = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(secret)
                    .Encode(userAuthClaim);

        return new AuthResponse(token);
    }
}
