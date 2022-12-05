#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202201132251 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "ApplicationUserAddresses",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                ApplicationUserId = table.Column<Guid>("TEXT", nullable: false),
                Firstname = table.Column<string>("TEXT", nullable: true),
                Surname = table.Column<string>("TEXT", nullable: true),
                PhoneNumber = table.Column<string>("TEXT", nullable: true),
                City = table.Column<string>("TEXT", nullable: true),
                Town = table.Column<string>("TEXT", nullable: true),
                District = table.Column<string>("TEXT", nullable: true),
                AddressDescription = table.Column<string>("TEXT", nullable: true),
                PostCode = table.Column<string>("TEXT", nullable: true),
                AddressName = table.Column<string>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationUserAddresses", x => x.Id);
                table.ForeignKey(
                    "FK_ApplicationUserAddresses_Users_ApplicationUserId",
                    x => x.ApplicationUserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_ApplicationUserAddresses_ApplicationUserId",
            "ApplicationUserAddresses",
            "ApplicationUserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ApplicationUserAddresses");
    }
}