using Microsoft.EntityFrameworkCore.Migrations;

namespace ShortIn_API.Migrations
{
    public partial class popularDB : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Url(fullUrl, shortUrl) Values ('www.google.com', 'lh1')");
            mb.Sql("Insert into Url(fullUrl, shortUrl) Values ('www.youtube.com', 'lh1')");
            mb.Sql("Insert into Url(fullUrl, shortUrl) Values ('www.ciandt.com', 'lh1')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Url");
        }
    }
}
