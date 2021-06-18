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
            try
            {
                if(objUrl == null)
                {
                    throw new ArgumentException("You need to send a correct object body");
                }

                if (!(objUrl.fullUrl.Contains("http://") || objUrl.fullUrl.Contains("https://")))
                {
                    throw new ArgumentException("You must send the url protocol");
                }


                objUrl.shortUrl = randomNumberGenerator();
                
                _urlRepo.Add(objUrl);
                _urlRepo.Commit();
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error trying to generate a new short url.", ex);
            }
            
        }

        public string GetUrl(string shortUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(shortUrl))
                {
                    var url = _urlRepo.GetById(p => p.shortUrl == shortUrl);
                    return url.fullUrl;
                }
                throw new ArgumentException("You must send a valid short Url");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting the url", ex);
            }            
        }

        private string randomNumberGenerator()
        {
            string rndNumber;

            do
            {
                rndNumber = random.Next(1000).ToString();
            }
            while ((_urlRepo.GetById(p => p.shortUrl == rndNumber)) != null);

            return rndNumber;
        }
    }
}
