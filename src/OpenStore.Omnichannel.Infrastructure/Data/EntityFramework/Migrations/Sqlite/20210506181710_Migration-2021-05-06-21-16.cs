using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Migrations.Sqlite;

public partial class Migration202105062116 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Brands",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true),
                Title = table.Column<string>("TEXT", nullable: true),
                DisplayTitle = table.Column<string>("TEXT", nullable: true),
                Description = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Brands", x => x.Id); });

        migrationBuilder.CreateTable(
            "Categories",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                ParentId = table.Column<Guid>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true),
                Title = table.Column<string>("TEXT", nullable: true),
                DisplayTitle = table.Column<string>("TEXT", nullable: true),
                Description = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
                table.ForeignKey(
                    "FK_Categories_Categories_ParentId",
                    x => x.ParentId,
                    "Categories",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "DataProtectionKeys",
            table => new
            {
                Id = table.Column<int>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FriendlyName = table.Column<string>("TEXT", nullable: true),
                Xml = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_DataProtectionKeys", x => x.Id); });

        migrationBuilder.CreateTable(
            "OpenIddictApplications",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                ClientId = table.Column<string>("TEXT", maxLength: 100, nullable: true),
                ClientSecret = table.Column<string>("TEXT", nullable: true),
                ConcurrencyToken = table.Column<string>("TEXT", maxLength: 50, nullable: true),
                ConsentType = table.Column<string>("TEXT", maxLength: 50, nullable: true),
                DisplayName = table.Column<string>("TEXT", nullable: true),
                DisplayNames = table.Column<string>("TEXT", nullable: true),
                Permissions = table.Column<string>("TEXT", nullable: true),
                PostLogoutRedirectUris = table.Column<string>("TEXT", nullable: true),
                Properties = table.Column<string>("TEXT", nullable: true),
                RedirectUris = table.Column<string>("TEXT", nullable: true),
                Requirements = table.Column<string>("TEXT", nullable: true),
                Type = table.Column<string>("TEXT", maxLength: 50, nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_OpenIddictApplications", x => x.Id); });

        migrationBuilder.CreateTable(
            "OpenIddictScopes",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                ConcurrencyToken = table.Column<string>("TEXT", maxLength: 50, nullable: true),
                Description = table.Column<string>("TEXT", nullable: true),
                Descriptions = table.Column<string>("TEXT", nullable: true),
                DisplayName = table.Column<string>("TEXT", nullable: true),
                DisplayNames = table.Column<string>("TEXT", nullable: true),
                Name = table.Column<string>("TEXT", maxLength: 191, nullable: true),
                Properties = table.Column<string>("TEXT", nullable: true),
                Resources = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_OpenIddictScopes", x => x.Id); });

        migrationBuilder.CreateTable(
            "OutBoxMessages",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Committed = table.Column<bool>("INTEGER", nullable: false),
                AggregateId = table.Column<string>("TEXT", nullable: false),
                Type = table.Column<string>("TEXT", nullable: false),
                Payload = table.Column<string>("TEXT", nullable: false),
                Timestamp = table.Column<DateTimeOffset>("TEXT", nullable: false),
                Version = table.Column<long>("INTEGER", nullable: false),
                CorrelationId = table.Column<string>("TEXT", nullable: true),
                CommittedBy = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_OutBoxMessages", x => x.Id); });

        migrationBuilder.CreateTable(
            "Roles",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Version = table.Column<long>("INTEGER", nullable: false),
                Name = table.Column<string>("TEXT", maxLength: 191, nullable: true),
                NormalizedName = table.Column<string>("TEXT", maxLength: 191, nullable: true),
                ConcurrencyStamp = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Roles", x => x.Id); });

        migrationBuilder.CreateTable(
            "SaleChannel",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Description = table.Column<string>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_SaleChannel", x => x.Id); });

        migrationBuilder.CreateTable(
            "Users",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Version = table.Column<long>("INTEGER", nullable: false),
                BirthDate = table.Column<DateTime>("TEXT", nullable: true),
                Name = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Surname = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Tckn = table.Column<string>("TEXT", maxLength: 11, nullable: true),
                PhotoPath = table.Column<string>("TEXT", nullable: true),
                Gender = table.Column<int>("INTEGER", nullable: true),
                RowVersion = table.Column<byte[]>("BLOB", rowVersion: true, nullable: true),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", maxLength: 255, nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", maxLength: 255, nullable: true),
                UserName = table.Column<string>("TEXT", maxLength: 191, nullable: true),
                NormalizedUserName = table.Column<string>("TEXT", maxLength: 191, nullable: true),
                Email = table.Column<string>("TEXT", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>("TEXT", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>("INTEGER", nullable: false),
                PasswordHash = table.Column<string>("TEXT", nullable: true),
                SecurityStamp = table.Column<string>("TEXT", nullable: true),
                ConcurrencyStamp = table.Column<string>("TEXT", nullable: true),
                PhoneNumber = table.Column<string>("TEXT", maxLength: 15, nullable: true),
                PhoneNumberConfirmed = table.Column<bool>("INTEGER", nullable: false),
                TwoFactorEnabled = table.Column<bool>("INTEGER", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>("TEXT", nullable: true),
                LockoutEnabled = table.Column<bool>("INTEGER", nullable: false),
                AccessFailedCount = table.Column<int>("INTEGER", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

        migrationBuilder.CreateTable(
            "BrandMedias",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                BrandId = table.Column<Guid>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true),
                Host = table.Column<string>("TEXT", nullable: true),
                Path = table.Column<string>("TEXT", nullable: true),
                Type = table.Column<string>("TEXT", nullable: true),
                Extension = table.Column<string>("TEXT", nullable: true),
                Filename = table.Column<string>("TEXT", nullable: true),
                Title = table.Column<string>("TEXT", nullable: true),
                Position = table.Column<int>("INTEGER", nullable: false),
                Size = table.Column<long>("INTEGER", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BrandMedias", x => x.Id);
                table.ForeignKey(
                    "FK_BrandMedias_Brands_BrandId",
                    x => x.BrandId,
                    "Brands",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Products",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                Handle = table.Column<string>("TEXT", maxLength: 512, nullable: false),
                Title = table.Column<string>("TEXT", maxLength: 255, nullable: false),
                Description = table.Column<string>("TEXT", nullable: true),
                HasMultipleVariants = table.Column<bool>("INTEGER", nullable: false),
                Status = table.Column<int>("INTEGER", nullable: false),
                MetaTitle = table.Column<string>("TEXT", nullable: true),
                MetaDescription = table.Column<string>("TEXT", nullable: true),
                Tags = table.Column<string>("TEXT", nullable: true),
                IsPhysicalProduct = table.Column<bool>("INTEGER", nullable: false),
                Weight = table.Column<decimal>("TEXT", precision: 9, scale: 2, nullable: true),
                WeightUnit = table.Column<string>("TEXT", nullable: true),
                HsCode = table.Column<string>("TEXT", nullable: true),
                BrandId = table.Column<Guid>("TEXT", nullable: true),
                Options = table.Column<string>("TEXT", nullable: true),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true),
                SoftDeleted = table.Column<bool>("INTEGER", nullable: false),
                Version = table.Column<long>("INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
                table.ForeignKey(
                    "FK_Products_Brands_BrandId",
                    x => x.BrandId,
                    "Brands",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "CategoryMedias",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                CategoryId = table.Column<Guid>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true),
                Host = table.Column<string>("TEXT", nullable: true),
                Path = table.Column<string>("TEXT", nullable: true),
                Type = table.Column<string>("TEXT", nullable: true),
                Extension = table.Column<string>("TEXT", nullable: true),
                Filename = table.Column<string>("TEXT", nullable: true),
                Title = table.Column<string>("TEXT", nullable: true),
                Position = table.Column<int>("INTEGER", nullable: false),
                Size = table.Column<long>("INTEGER", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CategoryMedias", x => x.Id);
                table.ForeignKey(
                    "FK_CategoryMedias_Categories_CategoryId",
                    x => x.CategoryId,
                    "Categories",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "OpenIddictAuthorizations",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                ApplicationId = table.Column<Guid>("TEXT", nullable: true),
                ConcurrencyToken = table.Column<string>("TEXT", maxLength: 50, nullable: true),
                CreationDate = table.Column<DateTime>("TEXT", nullable: true),
                Properties = table.Column<string>("TEXT", nullable: true),
                Scopes = table.Column<string>("TEXT", nullable: true),
                Status = table.Column<string>("TEXT", maxLength: 50, nullable: true),
                Subject = table.Column<string>("TEXT", maxLength: 400, nullable: true),
                Type = table.Column<string>("TEXT", maxLength: 50, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                table.ForeignKey(
                    "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                    x => x.ApplicationId,
                    "OpenIddictApplications",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "RoleClaims",
            table => new
            {
                Id = table.Column<int>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                RoleId = table.Column<Guid>("TEXT", nullable: false),
                ClaimType = table.Column<string>("TEXT", nullable: true),
                ClaimValue = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoleClaims", x => x.Id);
                table.ForeignKey(
                    "FK_RoleClaims_Roles_RoleId",
                    x => x.RoleId,
                    "Roles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "UserClaims",
            table => new
            {
                Id = table.Column<int>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<Guid>("TEXT", nullable: false),
                ClaimType = table.Column<string>("TEXT", nullable: true),
                ClaimValue = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserClaims", x => x.Id);
                table.ForeignKey(
                    "FK_UserClaims_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "UserLogins",
            table => new
            {
                LoginProvider = table.Column<string>("TEXT", nullable: false),
                ProviderKey = table.Column<string>("TEXT", nullable: false),
                ProviderDisplayName = table.Column<string>("TEXT", nullable: true),
                UserId = table.Column<Guid>("TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    "FK_UserLogins_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "UserRoles",
            table => new
            {
                UserId = table.Column<Guid>("TEXT", nullable: false),
                RoleId = table.Column<Guid>("TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    "FK_UserRoles_Roles_RoleId",
                    x => x.RoleId,
                    "Roles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_UserRoles_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "UserTokens",
            table => new
            {
                UserId = table.Column<Guid>("TEXT", nullable: false),
                LoginProvider = table.Column<string>("TEXT", nullable: false),
                Name = table.Column<string>("TEXT", nullable: false),
                Value = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    "FK_UserTokens_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "CategoryProducts",
            table => new
            {
                CategoryId = table.Column<Guid>("TEXT", nullable: false),
                ProductId = table.Column<Guid>("TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CategoryProducts", x => new { x.CategoryId, x.ProductId });
                table.ForeignKey(
                    "FK_CategoryProducts_Categories_CategoryId",
                    x => x.CategoryId,
                    "Categories",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CategoryProducts_Products_ProductId",
                    x => x.ProductId,
                    "Products",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ProductMedias",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                ProductId = table.Column<Guid>("TEXT", nullable: true),
                VariantIds = table.Column<string>("TEXT", nullable: true),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true),
                Host = table.Column<string>("TEXT", nullable: true),
                Path = table.Column<string>("TEXT", nullable: true),
                Type = table.Column<string>("TEXT", nullable: true),
                Extension = table.Column<string>("TEXT", nullable: true),
                Filename = table.Column<string>("TEXT", nullable: true),
                Title = table.Column<string>("TEXT", nullable: true),
                Position = table.Column<int>("INTEGER", nullable: false),
                Size = table.Column<long>("INTEGER", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductMedias", x => x.Id);
                table.ForeignKey(
                    "FK_ProductMedias_Products_ProductId",
                    x => x.ProductId,
                    "Products",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "SaleChannelProducts",
            table => new
            {
                ProductId = table.Column<Guid>("TEXT", nullable: false),
                SaleChannelId = table.Column<Guid>("TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SaleChannelProducts", x => new { x.SaleChannelId, x.ProductId });
                table.ForeignKey(
                    "FK_SaleChannelProducts_Products_ProductId",
                    x => x.ProductId,
                    "Products",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_SaleChannelProducts_SaleChannel_SaleChannelId",
                    x => x.SaleChannelId,
                    "SaleChannel",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Variant",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                ProductId = table.Column<Guid>("TEXT", nullable: false),
                Option1 = table.Column<string>("TEXT", nullable: true),
                Option2 = table.Column<string>("TEXT", nullable: true),
                Option3 = table.Column<string>("TEXT", nullable: true),
                Price = table.Column<decimal>("TEXT", precision: 18, scale: 2, nullable: false),
                CompareAtPrice = table.Column<decimal>("TEXT", precision: 18, scale: 2, nullable: true),
                Cost = table.Column<decimal>("TEXT", precision: 18, scale: 2, nullable: true),
                CalculateTaxAdditionally = table.Column<bool>("INTEGER", nullable: false),
                Sku = table.Column<string>("TEXT", maxLength: 255, nullable: true),
                Barcode = table.Column<string>("TEXT", maxLength: 255, nullable: true),
                TrackQuantity = table.Column<bool>("INTEGER", nullable: false),
                ContinueSellingWhenOutOfStock = table.Column<bool>("INTEGER", nullable: false),
                Version = table.Column<long>("INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                CreatedBy = table.Column<string>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedBy = table.Column<string>("TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Variant", x => x.Id);
                table.ForeignKey(
                    "FK_Variant_Products_ProductId",
                    x => x.ProductId,
                    "Products",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "OpenIddictTokens",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                ApplicationId = table.Column<Guid>("TEXT", nullable: true),
                AuthorizationId = table.Column<Guid>("TEXT", nullable: true),
                ConcurrencyToken = table.Column<string>("TEXT", maxLength: 50, nullable: true),
                CreationDate = table.Column<DateTime>("TEXT", nullable: true),
                ExpirationDate = table.Column<DateTime>("TEXT", nullable: true),
                Payload = table.Column<string>("TEXT", nullable: true),
                Properties = table.Column<string>("TEXT", nullable: true),
                RedemptionDate = table.Column<DateTime>("TEXT", nullable: true),
                ReferenceId = table.Column<string>("TEXT", maxLength: 100, nullable: true),
                Status = table.Column<string>("TEXT", maxLength: 50, nullable: true),
                Subject = table.Column<string>("TEXT", maxLength: 400, nullable: true),
                Type = table.Column<string>("TEXT", maxLength: 50, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                table.ForeignKey(
                    "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                    x => x.ApplicationId,
                    "OpenIddictApplications",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                    x => x.AuthorizationId,
                    "OpenIddictAuthorizations",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "Inventories",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                VariantId = table.Column<Guid>("TEXT", nullable: false),
                Quantity = table.Column<int>("INTEGER", nullable: false),
                AvailableQuantity = table.Column<int>("INTEGER", nullable: false),
                ContinueSellingWhenOutOfStock = table.Column<bool>("INTEGER", nullable: false),
                Version = table.Column<long>("INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Inventories", x => x.Id);
                table.ForeignKey(
                    "FK_Inventories_Variant_VariantId",
                    x => x.VariantId,
                    "Variant",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            "IX_BrandMedias_BrandId",
            "BrandMedias",
            "BrandId");

        migrationBuilder.CreateIndex(
            "IX_Categories_ParentId",
            "Categories",
            "ParentId");

        migrationBuilder.CreateIndex(
            "IX_CategoryMedias_CategoryId",
            "CategoryMedias",
            "CategoryId");

        migrationBuilder.CreateIndex(
            "IX_CategoryProducts_ProductId",
            "CategoryProducts",
            "ProductId");

        migrationBuilder.CreateIndex(
            "IX_Inventories_VariantId",
            "Inventories",
            "VariantId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_OpenIddictApplications_ClientId",
            "OpenIddictApplications",
            "ClientId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
            "OpenIddictAuthorizations",
            new[] { "ApplicationId", "Status", "Subject", "Type" });

        migrationBuilder.CreateIndex(
            "IX_OpenIddictScopes_Name",
            "OpenIddictScopes",
            "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
            "OpenIddictTokens",
            new[] { "ApplicationId", "Status", "Subject", "Type" });

        migrationBuilder.CreateIndex(
            "IX_OpenIddictTokens_AuthorizationId",
            "OpenIddictTokens",
            "AuthorizationId");

        migrationBuilder.CreateIndex(
            "IX_OpenIddictTokens_ReferenceId",
            "OpenIddictTokens",
            "ReferenceId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ProductMedias_ProductId",
            "ProductMedias",
            "ProductId");

        migrationBuilder.CreateIndex(
            "IX_Products_BrandId",
            "Products",
            "BrandId");

        migrationBuilder.CreateIndex(
            "IX_Products_Handle",
            "Products",
            "Handle",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_RoleClaims_RoleId",
            "RoleClaims",
            "RoleId");

        migrationBuilder.CreateIndex(
            "RoleNameIndex",
            "Roles",
            "NormalizedName",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_SaleChannelProducts_ProductId",
            "SaleChannelProducts",
            "ProductId");

        migrationBuilder.CreateIndex(
            "IX_UserClaims_UserId",
            "UserClaims",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_UserLogins_UserId",
            "UserLogins",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_UserRoles_RoleId",
            "UserRoles",
            "RoleId");

        migrationBuilder.CreateIndex(
            "EmailIndex",
            "Users",
            "NormalizedEmail");

        migrationBuilder.CreateIndex(
            "UserNameIndex",
            "Users",
            "NormalizedUserName",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Variant_Barcode",
            "Variant",
            "Barcode");

        migrationBuilder.CreateIndex(
            "IX_Variant_ProductId",
            "Variant",
            "ProductId");

        migrationBuilder.CreateIndex(
            "IX_Variant_Sku",
            "Variant",
            "Sku");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "BrandMedias");

        migrationBuilder.DropTable(
            "CategoryMedias");

        migrationBuilder.DropTable(
            "CategoryProducts");

        migrationBuilder.DropTable(
            "DataProtectionKeys");

        migrationBuilder.DropTable(
            "Inventories");

        migrationBuilder.DropTable(
            "OpenIddictScopes");

        migrationBuilder.DropTable(
            "OpenIddictTokens");

        migrationBuilder.DropTable(
            "OutBoxMessages");

        migrationBuilder.DropTable(
            "ProductMedias");

        migrationBuilder.DropTable(
            "RoleClaims");

        migrationBuilder.DropTable(
            "SaleChannelProducts");

        migrationBuilder.DropTable(
            "UserClaims");

        migrationBuilder.DropTable(
            "UserLogins");

        migrationBuilder.DropTable(
            "UserRoles");

        migrationBuilder.DropTable(
            "UserTokens");

        migrationBuilder.DropTable(
            "Categories");

        migrationBuilder.DropTable(
            "Variant");

        migrationBuilder.DropTable(
            "OpenIddictAuthorizations");

        migrationBuilder.DropTable(
            "SaleChannel");

        migrationBuilder.DropTable(
            "Roles");

        migrationBuilder.DropTable(
            "Users");

        migrationBuilder.DropTable(
            "Products");

        migrationBuilder.DropTable(
            "OpenIddictApplications");

        migrationBuilder.DropTable(
            "Brands");
    }
}