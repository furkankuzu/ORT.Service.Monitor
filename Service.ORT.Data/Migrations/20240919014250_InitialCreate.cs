using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.ORT.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Website",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    IsUp = table.Column<bool>(type: "bit", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Website", x => x.Id);
                });

            migrationBuilder.Sql(@"
  insert into Website(WebsiteUrl,IsUp,Period,UserEmail) values ('https://www.ortellium.com',0,5,'a@b.c')
  insert into Website(WebsiteUrl,IsUp,Period,UserEmail) values ('https://www.google.com',0,3,'a@b.c')
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Website");
        }
    }
}
