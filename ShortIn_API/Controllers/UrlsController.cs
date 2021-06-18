﻿using Microsoft.AspNetCore.Mvc;
using ShortIn_API.Domain;
using ShortIn_API.Integration;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShortIn_API.Controllers
{
    [ApiController]
    [Route("api")]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlBusiness _urlBusiness;

        public UrlsController(IUrlBusiness context)
        {
            _urlBusiness = context;
        }


        // GET: short/urlCurta
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetUrl([FromRoute(Name = "id")]string shortUrl)
        {   
            var url = _urlBusiness.GetUrl(shortUrl);
            return Redirect(url);            

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
