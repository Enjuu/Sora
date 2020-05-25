using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sora.Database;
using Sora.Database.Models;
using PlayMode = Sora.Enums.PlayMode;

namespace Sora.Controllers.Web
{
    [ApiController]
    [Route("/web/")]
    public class Replay : Controller
    {
        #region GET /web/osu-getreplay.php

        [HttpGet("osu-getreplay.php")]
        public async Task<IActionResult> GetReplay(
            [FromQuery(Name = "c")] int replayId,
            [FromQuery(Name = "m")] PlayMode mode,
            [FromQuery(Name = "u")] string userName,
            [FromQuery(Name = "h")] string pass,
            [FromServices] SoraDbContext ctx
        )
        {
            var user = await DbUser.GetDbUser(ctx, userName);
            if (user == null)
                return Ok("err: pass");

            if (!user.IsPassword(pass))
                return Ok("err: pass");

            var s = await DbScore.GetScore(ctx, replayId);
            if (s == null)
                return NotFound();

            return File(System.IO.File.OpenRead("data/replays/" + s.ReplayMd5), "binary/octet-stream", s.ReplayMd5);
        }

        #endregion
    }
}