using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logger = Sora.Utilities.Logger;
using Pisstaube = Sora.Utilities.Pisstaube;

namespace Sora.Controllers.Web
{
    [ApiController]
    [Route("/web/")]
    public class BeatmapDownloader : Controller
    {
        #region GET /web/maps/*

        [HttpGet("/web/maps/{map}")]
        public async Task<IActionResult> GetBeatmap(
            string map,
            [FromServices] Pisstaube pisstaube)
        {
            Logger.Debug(map);
            if (!Directory.Exists("data/beatmaps"))
                Directory.CreateDirectory("data/beatmaps");

            var beatmap = await pisstaube.DownloadBeatmapAsync(map, false);

            // No config reading for you :3
            map = map.Replace("..", string.Empty);
            if (!System.IO.File.Exists(beatmap))
                return NotFound($"Could not find Beatmap with the Name of {map}");

            return File(System.IO.File.OpenRead(beatmap), "text/html");
        }

        #endregion
    }
}