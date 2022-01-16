using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite
{
    public partial class Migration202201161711 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserAddresses_Users_ApplicationUserId",
                table: "ApplicationUserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserAddresses",
                table: "ApplicationUserAddresses");

            migrationBuilder.RenameTable(
                name: "ApplicationUserAddresses",
                newName: "UserAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserAddresses_ApplicationUserId",
                table: "UserAddresses",
                newName: "IX_UserAddresses_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Users_ApplicationUserId",
                table: "UserAddresses",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Users_ApplicationUserId",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "ApplicationUserAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddresses_ApplicationUserId",
                table: "ApplicationUserAddresses",
                newName: "IX_ApplicationUserAddresses_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserAddresses",
                table: "ApplicationUserAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserAddresses_Users_ApplicationUserId",
                table: "ApplicationUserAddresses",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
