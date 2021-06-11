using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortIn_API.Domain;
using ShortIn_API.Domain.Context;
using ShortIn_API.Integration;

namespace ShortIn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlRepository _urlRepo;

        Random random = new Random();

        public UrlsController(IUrlRepository context)
        {
            _urlRepo = context;
        }

        // GET: api/Urls
        [HttpGet]
        public ActionResult<IEnumerable<Url>> GetUrls()
        {
            return _urlRepo.GetUrlById().ToList();
        }

        // GET: api/Urls/5
        [HttpGet("{id}")]
        public ActionResult<string> GetUrl(string shortUrl)
        {

            var url = _urlRepo.GetById(p => p.shortUrl == shortUrl);

            if (url == null)
            {
                return NotFound();
            }

            var redirecionar = url.fullUrl;

            //inserir validação
            return Redirect("http://"+redirecionar);
        }


        // PUT: api/Urls/5
        [HttpPut("{id}")]
        public IActionResult PutUrl(int id, Url url)
        {
            if (id != url.urlId)
            {
                return BadRequest();
            }

            _urlRepo.Update(url);
            _urlRepo.Commit();      
            return Ok();
        }

        // POST: api/Urls
        [HttpPost]
        public ActionResult<Url> PostUrl([FromBody]Url url)
        {
            url.shortUrl = random.Next(1000).ToString();
            _urlRepo.Add(url);
            _urlRepo.Commit();


            return CreatedAtAction("GetUrl", new { id = url.urlId }, url);
        }

        // DELETE: api/Urls/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUrl(int id)
        {
            var url = _urlRepo.GetById(p => p.urlId == id);

            if (url == null)
            {
                return NotFound();
            }

            _urlRepo.Delete(url);
            _urlRepo.Commit();

            return NoContent();
        }
    }
}
