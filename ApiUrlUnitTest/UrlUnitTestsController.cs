using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortIn_API.Controllers;
using ShortIn_API.Domain;
using ShortIn_API.Domain.Context;
using ShortIn_API.Integration;
using System;
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
        public void GetUrl_Catch_EmptyUrlObject()
        {
            //Arrange
            var controller = new UrlsController(_urlBusiness);
            string urlShortlink = "";

            //Act

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() => controller.GetUrl(urlShortlink));
            Assert.Equal("You must send a valid short Url", exception.Message);
        }

        [Fact]
        public void GetUrl_Catch_ErrorGettingUrl()
        {
            //Arrange
            var controller = new UrlsController(_urlBusiness);
            string urlShortlink = null;

            //Act

            //Assert
            Exception exception = Assert.Throws<Exception>(() => controller.GetUrl(urlShortlink));
            Assert.Equal("Error while getting the url", exception.Message);
        }

        [Fact]
        public void PostUrl_Return_SucessStatusCode()
        {
            //Arrange
            var controller = new UrlsController(_urlBusiness);
            Url url = new Url
            {
                fullUrl = "https://www.youtube.com/watch?v=sCLto40lisY"
            };

            //Act
            var data = controller.PostUrl(url) as CreatedAtRouteResult;

            //Assert
            Assert.NotNull(data);
            Assert.Equal(201, data.StatusCode);
        }

        [Fact]
        public void PostUrl_CatchArgumentException()
        {
            //Arrange
            var controller = new UrlsController(_urlBusiness);
            Url url = new Url
            {
                fullUrl = "www.google.com"
            };

            //Act
            
            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() => controller.PostUrl(url));
            Assert.Equal("You must send the url protocol", exception.Message);
            
        }

        [Fact]
        public void PostUrl_CatchException()
        {
            //Arrange
            var controller = new UrlsController(_urlBusiness);
            Url url = new Url
            {
                fullUrl = null
            };

            //Act

            //Assert
            Exception exception = Assert.Throws<Exception>(() => controller.PostUrl(url));
            Assert.Equal("Error trying to generate a new short url.", exception.Message);

        }

        [Fact]
        public void PostUrl_CatchNullUrlObject()
        {
            //Arrange
            var controller = new UrlsController(_urlBusiness);
            Url url = null;

            //Act

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() => controller.PostUrl(url));
            Assert.Equal("You need to send a correct object body", exception.Message);

        }





    }
}
