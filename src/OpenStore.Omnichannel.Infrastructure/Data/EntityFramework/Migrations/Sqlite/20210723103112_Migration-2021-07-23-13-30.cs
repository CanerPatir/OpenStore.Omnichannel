using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202107231330 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "StorePreferences",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", nullable: true),
                Contact_Address = table.Column<string>("TEXT", nullable: true),
                Contact_Email = table.Column<string>("TEXT", nullable: true),
                Contact_Phone = table.Column<string>("TEXT", nullable: true),
                Contact_CopyrightText = table.Column<string>("TEXT", nullable: true),
                Contact_FacebookUrl = table.Column<string>("TEXT", nullable: true),
                Contact_TwitterUrl = table.Column<string>("TEXT", nullable: true),
                Contact_InstagramUrl = table.Column<string>("TEXT", nullable: true),
                Contact_YoutubeUrl = table.Column<string>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_StorePreferences", x => x.Id); });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "StorePreferences");
    }
}