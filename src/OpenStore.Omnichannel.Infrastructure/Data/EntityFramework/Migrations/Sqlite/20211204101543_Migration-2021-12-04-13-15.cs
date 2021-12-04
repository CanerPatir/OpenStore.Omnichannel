using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite
{
    public partial class Migration202112041315 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductCollections",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Media_Extension",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Media_Filename",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Media_Host",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Media_Path",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Media_Position",
                table: "ProductCollections",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Media_Size",
                table: "ProductCollections",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Media_Title",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Media_Type",
                table: "ProductCollections",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Media_Extension",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Media_Filename",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Media_Host",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Media_Path",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Media_Position",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Media_Size",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Media_Title",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Media_Type",
                table: "ProductCollections");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ProductCollections",
                newName: "Name");
        }
    }
}
