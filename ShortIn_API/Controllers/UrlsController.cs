using Microsoft.AspNetCore.Mvc;
using ShortIn_API.Domain;
using ShortIn_API.Integration;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShortIn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlBusiness _urlBusiness;

        public UrlsController(IUrlBusiness context)
        {
            _urlBusiness = context;
        }


        // GET: short/urlCurta
        [HttpGet]
        [Route("short/{id}")]
        public ActionResult<string> GetUrl(string shortUrl)
        {   
            return Redirect(_urlBusiness.GetUrl(shortUrl));
        }

        // POST: api/Urls
        [HttpPost]
        public ActionResult<Url> PostUrl([FromBody][Required]Url url)
        {
            _urlBusiness.CreateShortURL(url);

            return CreatedAtAction("GetUrl", new { id = url.urlId }, url);
        }
    }
}
