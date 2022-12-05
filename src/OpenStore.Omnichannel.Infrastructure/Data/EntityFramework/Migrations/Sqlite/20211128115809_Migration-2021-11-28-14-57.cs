#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202111281457 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "ShoppingCarts",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                UserId = table.Column<Guid>("TEXT", nullable: true),
                IsAuthenticated = table.Column<bool>("INTEGER", nullable: false),
                Items = table.Column<string>("TEXT", nullable: true),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ShoppingCarts", x => x.Id); });

        migrationBuilder.CreateIndex(
            "IX_ShoppingCarts_UserId",
            "ShoppingCarts",
            "UserId",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ShoppingCarts");
    }
}