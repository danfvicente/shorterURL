using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortIn_API.Controllers;
using ShortIn_API.Domain;
using ShortIn_API.Domain.Context;
using ShortIn_API.Integration;
using Xunit;

namespace ApiUrlUnitTest
{
    public class UrlUnitTestsController
    {
        private readonly IUrlBusiness _urlBusiness;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionString = "Server=localhost;DataBase=Url;Integrated Security = True;";

        static UrlUnitTestsController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options;
        }

        public UrlUnitTestsController()
        {
            var context = new AppDbContext(dbContextOptions);

            //DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
            //db.Seed(context)

            IUrlRepository _repo = new UrlRepository(context);
            _urlBusiness = new UrlBusiness(_repo);
        }
        
        //Testar se a URL curta está redirecionado para a URL longa.
        [Fact]
        public void GetUrl_Return_Redirect()
        {
            //Arrange
            var controller = new UrlsController(_urlBusiness);
            var urlShortlink = "903";

            //Act
            var data = controller.GetUrl(urlShortlink);

            //Assert
            Assert.IsAssignableFrom<RedirectResult>(data);
        }

        [Fact]
        public void PostUrl_Return_CreatedResult()
        {
            //Arrange
            var controller = new UrlsController(_urlBusiness);
            Url url = new Url
            {
                fullUrl = "https://www.youtube.com/watch?v=sCLto40lisY"
            };

            //Act
            var data = controller.PostUrl(url);

            //Assert
            Assert.IsType<bool>(data);
        }



    }
}
