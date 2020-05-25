using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Cache = Sora.Allocation.Cache;

namespace Sora.Controllers.Web
{
    [ApiController]
    [Route("/web/")]
    public class Updater : Controller
    {
        #region GET /web/check-updates.php

        [HttpGet("check-updates.php")]
        public IActionResult CheckUpdates(
            [FromQuery] string action,
            [FromQuery(Name = "stream")] string qstream,
            [FromQuery] ulong time,
            [FromServices] Cache cache)
        {
            if (cache.TryGet("sora:updater:" + action + qstream, out string answer))
                return Ok(answer);

            var request = (HttpWebRequest) WebRequest.Create(
                $"https://1.1.1.1/web/check-updates.php?action={action}&stream={qstream}&time={time}"
            );
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Host = "osu.ppy.sh";
            request.UserAgent = "osu";

            using var response = (HttpWebResponse) request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream ?? throw new Exception("Request Failed!"));

            var result = reader.ReadToEnd();
            cache.Set("sora:updater:" + action + qstream, result, TimeSpan.FromDays(1));
            return Ok(result);
        }

        #endregion
    }
}