using Microsoft.AspNetCore.Mvc;

namespace Sora.Controllers.Web
{
    [ApiController]
    [Route("/web/")]
    public class IndexController : Controller
    {
        #region GET /web/

        [HttpGet]
        public IActionResult Index() => Ok("ERR: you sneaky little mouse :3");

        #endregion
    }
}