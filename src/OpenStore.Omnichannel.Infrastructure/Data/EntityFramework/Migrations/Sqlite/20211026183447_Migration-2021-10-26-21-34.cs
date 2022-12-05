#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202110262134 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "ContinueSellingWhenOutOfStock",
            "Variant");

        migrationBuilder.AddColumn<DateTime>(
            "CreatedAt",
            "Inventories",
            "TEXT",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            "CreatedBy",
            "Inventories",
            "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            "UpdatedAt",
            "Inventories",
            "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "UpdatedBy",
            "Inventories",
            "TEXT",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "CreatedAt",
            "Inventories");

        migrationBuilder.DropColumn(
            "CreatedBy",
            "Inventories");

        migrationBuilder.DropColumn(
            "UpdatedAt",
            "Inventories");

        migrationBuilder.DropColumn(
            "UpdatedBy",
            "Inventories");

        migrationBuilder.AddColumn<bool>(
            "ContinueSellingWhenOutOfStock",
            "Variant",
            "INTEGER",
            nullable: false,
            defaultValue: false);
    }
}