using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite
{
    public partial class Migration202112041458 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProductCollections",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Handle",
                table: "ProductCollections",
                type: "TEXT",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCollections_Handle",
                table: "ProductCollections",
                column: "Handle",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCollections_Handle",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Handle",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "ProductCollections");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255);
        }
    }
}
