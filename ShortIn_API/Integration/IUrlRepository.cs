using ShortIn_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortIn_API.Integration
{
    public interface IUrlRepository : IRepository<Url>
    {
        IEnumerable<Url> GetUrlById();
        IEnumerable<Url> GetByShortUrl();
        void Commit();
    }
}
