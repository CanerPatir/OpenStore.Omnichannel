#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202112022303 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "ProductCollections",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", nullable: true),
                Description = table.Column<string>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_ProductCollections", x => x.Id); });

        migrationBuilder.CreateTable(
            "ProductCollectionItem",
            table => new
            {
                ProductCollectionId = table.Column<Guid>("TEXT", nullable: false),
                ProductId = table.Column<Guid>("TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductCollectionItem", x => new { x.ProductCollectionId, x.ProductId });
                table.ForeignKey(
                    "FK_ProductCollectionItem_ProductCollections_ProductCollectionId",
                    x => x.ProductCollectionId,
                    "ProductCollections",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_ProductCollectionItem_Products_ProductId",
                    x => x.ProductId,
                    "Products",
                    "Id");
            });

        migrationBuilder.CreateIndex(
            "IX_ProductCollectionItem_ProductId",
            "ProductCollectionItem",
            "ProductId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ProductCollectionItem");

        migrationBuilder.DropTable(
            "ProductCollections");
    }
}