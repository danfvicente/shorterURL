using ShortIn_API.Domain;
using ShortIn_API.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortIn_API.Integration
{
    public class UrlRepository : Repository<Url>, IUrlRepository
    {
        public new AppDbContext _context;
        public UrlRepository(AppDbContext contexto) : base(contexto)
        {
            _context = contexto;
        }
        public IEnumerable<Url> GetUrlById()
        {
            return Get().OrderBy(c => c.urlId).ToList();
        }

        public IEnumerable<Url> GetByShortUrl()
        {
            return Get().OrderBy(c => c.shortUrl).ToList();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
