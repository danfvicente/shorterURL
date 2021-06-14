using ShortIn_API.Integration;
using System;

namespace ShortIn_API.Domain
{
    public class UrlBusiness : IUrlBusiness
    {
        private readonly IUrlRepository _urlRepo;

        public UrlBusiness(IUrlRepository context)
        {
            _urlRepo = context;
        }

        Random random = new Random();

        public void CreateShortURL(Url objUrl)
        {
            objUrl.shortUrl = random.Next(1000).ToString();
            _urlRepo.Add(objUrl);
            _urlRepo.Commit();
        }

        public string GetUrl(string shortUrl)
        {
            var url = _urlRepo.GetById(p => p.shortUrl == shortUrl);

            return url.fullUrl;
        }
    }
}
