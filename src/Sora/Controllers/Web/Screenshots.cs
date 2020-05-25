using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Crypto = Sora.Utilities.Crypto;

namespace Sora.Controllers.Web
{
    [ApiController]
    [Route("/web/")]
    public class Screenshots : Controller
    {
        #region POST /web/osu-screenshot.php

        [HttpPost("osu-screenshot.php")]
        public IActionResult UploadScreenshot([FromServices] Config config)
        {
            if (!Directory.Exists("data/screenshots"))
                Directory.CreateDirectory("data/screenshots");

            var screenshot = Request.Form.Files.GetFile("ss");
            var rand = Crypto.RandomString(16);

            using var stream = screenshot.OpenReadStream();
            using var fs = System.IO.File.OpenWrite($"data/screenshots/{rand}");

            Image.FromStream(stream)
                .Save(fs, ImageFormat.Jpeg);

            return Ok($"http://{config.Server.ScreenShotHostname}/ss/{rand}");
        }

        #endregion
    }
}