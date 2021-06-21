using ShortIn_API.Domain;
using ShortIn_API.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUrlUnitTest
{
    public class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        {

        }

        public void Seed(AppDbContext context)
        {
            context.Urls.Add(new Url { urlId = 101, fullUrl = "https://github.com/", shortUrl = "881" });
            context.Urls.Add(new Url { urlId = 102, fullUrl = "https://www.youtube.com/watch?v=yKNxeF4KMsY", shortUrl = "882" });
            context.Urls.Add(new Url { urlId = 103, fullUrl = "https://www.youtube.com/watch?v=fXw0jcYbqdo", shortUrl = "883" });
        }

    }
}
