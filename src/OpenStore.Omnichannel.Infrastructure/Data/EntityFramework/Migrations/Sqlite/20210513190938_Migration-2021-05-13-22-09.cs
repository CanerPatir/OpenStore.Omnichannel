using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202105132209 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Inventories_Variant_VariantId",
            "Inventories");

        migrationBuilder.AddForeignKey(
            "FK_Inventories_Variant_VariantId",
            "Inventories",
            "VariantId",
            "Variant",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Inventories_Variant_VariantId",
            "Inventories");

        migrationBuilder.AddForeignKey(
            "FK_Inventories_Variant_VariantId",
            "Inventories",
            "VariantId",
            "Variant",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}