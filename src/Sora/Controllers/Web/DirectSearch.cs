using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sora.Database;
using Sora.Database.Models;
using Pisstaube = Sora.Utilities.Pisstaube;

namespace Sora.Controllers.Web
{
    [ApiController]
    [Route("/web/")]
    public class DirectSearch : Controller
    {
        #region GET /web/osu-search.php

        [HttpGet("osu-search.php")]
        public async Task<IActionResult> GetSearchDirect(
            [FromQuery(Name = "m")] int playMode,
            [FromQuery(Name = "r")] int rankedStatus,
            [FromQuery(Name = "p")] int page,
            [FromQuery(Name = "q")] string query,
            [FromQuery(Name = "u")] string userName,
            [FromQuery(Name = "h")] string pass,
            [FromServices] SoraDbContext ctx,
            [FromServices] Pisstaube pisstaube
        )
        {
            Response.ContentType = "text/plain";
            var user = await DbUser.GetDbUser(ctx, userName);
            if (user == null)
                return Ok("err: pass");

            if (!user.IsPassword(pass))
                return Ok("err: pass");

            var searchResult = await pisstaube.SearchDirectAsync(query, rankedStatus, playMode, page);

            return Ok(searchResult); // this no longer needs to be cached...
        }

        #endregion

        #region GET /web/osu-search-set.php

        [HttpGet("osu-search-set.php")]
        public async Task<IActionResult> GetDirectNp(
            [FromQuery(Name = "s")] int setId,
            [FromQuery(Name = "b")] int beatmapId,
            [FromQuery(Name = "u")] string userName,
            [FromQuery(Name = "h")] string pass,
            [FromServices] SoraDbContext ctx,
            [FromServices] Pisstaube pisstaube
        )
        {
            Response.ContentType = "text/plain";

            var user = await DbUser.GetDbUser(ctx, userName);
            if (user == null)
                return Ok("err: pass");

            if (!user.IsPassword(pass))
                return Ok("err: pass");

            return Ok(await (setId != 0
                ? pisstaube.FetchDirectBeatmapSetAsync(setId)
                : pisstaube.FetchDirectBeatmapAsync(beatmapId)));
        }

        #endregion
    }
}