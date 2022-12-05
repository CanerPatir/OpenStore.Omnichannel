#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202112041458 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            "Title",
            "ProductCollections",
            "TEXT",
            maxLength: 255,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AddColumn<string>(
            "Handle",
            "ProductCollections",
            "TEXT",
            maxLength: 512,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            "MetaDescription",
            "ProductCollections",
            "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "MetaTitle",
            "ProductCollections",
            "TEXT",
            nullable: true);

        migrationBuilder.CreateIndex(
            "IX_ProductCollections_Handle",
            "ProductCollections",
            "Handle",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            "IX_ProductCollections_Handle",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "Handle",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "MetaDescription",
            "ProductCollections");

        migrationBuilder.DropColumn(
            "MetaTitle",
            "ProductCollections");

        migrationBuilder.AlterColumn<string>(
            "Title",
            "ProductCollections",
            "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldMaxLength: 255);
    }
}