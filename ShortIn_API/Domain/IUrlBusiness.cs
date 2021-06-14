namespace ShortIn_API.Domain
{
    public interface IUrlBusiness
    {
        public void CreateShortURL(Url url);
        public string GetUrl(string url);
    }
}
