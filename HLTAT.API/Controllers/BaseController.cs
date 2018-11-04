using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HLTAT.API.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        public virtual object ReturnErrorCode(string message)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return new { data = message, status = StatusCodes.Status500InternalServerError };
        }


        [NonAction]
        public override JsonResult Json(object data)
        {
            return new JsonResult(data, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}