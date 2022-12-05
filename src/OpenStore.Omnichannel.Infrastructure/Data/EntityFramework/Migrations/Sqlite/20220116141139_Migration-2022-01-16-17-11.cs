#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202201161711 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_ApplicationUserAddresses_Users_ApplicationUserId",
            "ApplicationUserAddresses");

        migrationBuilder.DropPrimaryKey(
            "PK_ApplicationUserAddresses",
            "ApplicationUserAddresses");

        migrationBuilder.RenameTable(
            "ApplicationUserAddresses",
            newName: "UserAddresses");

        migrationBuilder.RenameIndex(
            "IX_ApplicationUserAddresses_ApplicationUserId",
            table: "UserAddresses",
            newName: "IX_UserAddresses_ApplicationUserId");

        migrationBuilder.AddPrimaryKey(
            "PK_UserAddresses",
            "UserAddresses",
            "Id");

        migrationBuilder.AddForeignKey(
            "FK_UserAddresses_Users_ApplicationUserId",
            "UserAddresses",
            "ApplicationUserId",
            "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_UserAddresses_Users_ApplicationUserId",
            "UserAddresses");

        migrationBuilder.DropPrimaryKey(
            "PK_UserAddresses",
            "UserAddresses");

        migrationBuilder.RenameTable(
            "UserAddresses",
            newName: "ApplicationUserAddresses");

        migrationBuilder.RenameIndex(
            "IX_UserAddresses_ApplicationUserId",
            table: "ApplicationUserAddresses",
            newName: "IX_ApplicationUserAddresses_ApplicationUserId");

        migrationBuilder.AddPrimaryKey(
            "PK_ApplicationUserAddresses",
            "ApplicationUserAddresses",
            "Id");

        migrationBuilder.AddForeignKey(
            "FK_ApplicationUserAddresses_Users_ApplicationUserId",
            "ApplicationUserAddresses",
            "ApplicationUserId",
            "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}