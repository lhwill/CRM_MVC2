using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CRM_MVC.Controllers
{
    public class ErrorsController : Controller
    {
        private IHostingEnvironment _env;

        public ErrorsController(IHostingEnvironment env)
        {
            _env = env;
        }

        [Route("errors/{statusCode}")]
        public IActionResult CustomError(int statusCode)
        {
            var filePath = $"{_env.WebRootPath}/{(statusCode == 404 ? 404 : 500)}.html";
            return new PhysicalFileResult(filePath, new MediaTypeHeaderValue("text/html"));
        }
    }
}