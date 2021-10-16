using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202107231330 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "StorePreferences",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: true),
                Contact_Address = table.Column<string>(type: "TEXT", nullable: true),
                Contact_Email = table.Column<string>(type: "TEXT", nullable: true),
                Contact_Phone = table.Column<string>(type: "TEXT", nullable: true),
                Contact_CopyrightText = table.Column<string>(type: "TEXT", nullable: true),
                Contact_FacebookUrl = table.Column<string>(type: "TEXT", nullable: true),
                Contact_TwitterUrl = table.Column<string>(type: "TEXT", nullable: true),
                Contact_InstagramUrl = table.Column<string>(type: "TEXT", nullable: true),
                Contact_YoutubeUrl = table.Column<string>(type: "TEXT", nullable: true),
                Version = table.Column<long>(type: "INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_StorePreferences", x => x.Id); });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "StorePreferences");
    }
}