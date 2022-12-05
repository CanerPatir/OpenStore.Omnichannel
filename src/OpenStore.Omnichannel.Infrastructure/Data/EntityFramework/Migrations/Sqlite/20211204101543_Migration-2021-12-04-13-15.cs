#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202112041315 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            "Name",
            "ProductCollections",
            "Title");

        migrationBuilder.AddColumn<string>(
            "Media_Extension",
            "ProductCollections",
            "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Media_Filename",
            "ProductCollections",
            "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Media_Host",
            "ProductCollections",
            "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Media_Path",
            "ProductCollections",
            "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            "Media_Position",
            "ProductCollections",
            "INTEGER",
            nullable: true);

        migrationBuilder.AddColumn<long>(
            "Media_Size",
            "ProductCollections",
            "INTEGER",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Media_Title",
            "ProductCollections",
            "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Media_Type",
            "ProductCollections",
            "TEXT",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "Media_Extension",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "Media_Filename",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "Media_Host",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "Media_Path",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "Media_Position",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "Media_Size",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "Media_Title",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "Media_Type",
            "ProductCollections");

        migrationBuilder.RenameColumn(
            "Title",
            "ProductCollections",
            "Name");
    }
}