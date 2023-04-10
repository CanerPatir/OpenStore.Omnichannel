using System.Globalization;
using Microsoft.EntityFrameworkCore;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;
using OpenStore.Omnichannel.Shared.Command.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Tools.Helpers;
using Sylvan.Data.Csv;

namespace OpenStore.Omnichannel.Tools;

public class SampleProductDataFeeder : BackgroundService
{
    private const bool Download = false;
    private readonly ILogger<SampleProductDataFeeder> _logger;
    private readonly IServiceProvider _serviceProvider;

    public SampleProductDataFeeder(ILogger<SampleProductDataFeeder> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting...");
        _logger.LogInformation("Extracting");
        var productCsvRows = await ExtractFromCsv(stoppingToken);
        _logger.LogInformation("Extracted");
        _logger.LogInformation("Grouping...");
        var groupedProductRows = GroupByParent(productCsvRows);
        _logger.LogInformation("Grouped");
        _logger.LogInformation("Group Count: {}", groupedProductRows.Count);
        _logger.LogInformation("Transforming...");
        await Transform(groupedProductRows);
        _logger.LogInformation("Transformed");
        _logger.LogInformation("Finished...");
    }

    private async Task Transform(Dictionary<ProductCsvRow, List<ProductCsvRow>> groupedProductRows)
    {
        using var scope = _serviceProvider.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        foreach (var (mainProduct, variants) in groupedProductRows)
        {
            if (await dbContext.Products.AnyAsync(x => x.Title == mainProduct.Name))
                continue;

            try
            {
                await ProceedItem(dbContext, mainProduct, variants);
            }
            catch (Exception e)
            {
                _logger.LogError(new Exception($"{mainProduct.Sku} product could not be proceed", e), $"{mainProduct.Sku} product could not be proceed");
                throw;
            }
        }

        async Task ProceedItem(ApplicationDbContext context, ProductCsvRow mainProduct, List<ProductCsvRow> variants)
        {
            var medias = new List<ProductMedia>();
            foreach (var variantProduct in variants)
            {
                medias.AddRange(await DownloadImage(context, variantProduct.Images.Split(","), variantProduct.Guid));
            }

            await context.SaveChangesAsync();

            var product = context.Products.Add(
                Product.Create(
                    new CreateProduct(new ProductDto
                    {
                        Id = Guid.NewGuid(),
                        Handle = new SlugHelper().GenerateSlug(mainProduct.Name),
                        Title = mainProduct.Name,
                        Description = mainProduct.Description,
                        HasMultipleVariants = mainProduct.Type == ProductCsvRowType.variable,
                        IsPhysicalProduct = true,
                        Status = ProductStatus.Active,
                        MetaTitle = mainProduct.Name,
                        MetaDescription = mainProduct.ShortDescription,
                        Tags = mainProduct.Tags,
                        Weight = mainProduct.WeightLbs,
                        WeightUnit = "lbs",
                        Options = GetOptions(mainProduct),
                        Medias = medias.Select(m => new ProductMediaDto
                        {
                            Id = m.Id,
                            Path = m.Path,
                            Title = m.Title,
                            Type = m.Type,
                            Extension = m.Extension,
                            Filename = m.Filename,
                            Position = m.Position,
                            Host = m.Host,
                            Size = m.Size,
                            VariantIds = m.VariantIds.ToHashSet()
                        }),
                        Variants = variants.Select(v => new VariantDto
                        {
                            Id = v.Guid,
                            Barcode = v.Sku,
                            Sku = v.Sku,
                            Option1 = v.Attribute1Values,
                            Option2 = v.Attribute2Values,
                            Option3 = v.Attribute3Values,
                            Price = v.RegularPrice,
                            CompareAtPrice = v.SalePrice,
                            Quantity = v.Stock,
                            TrackQuantity = true,
                            CalculateTaxAdditionally = v.TaxClass != null,
                            ContinueSellingWhenOutOfStock = false,
                        }).ToList(),
                    }), id => medias.FirstOrDefault(x => x.Id == id)
                )
            ).Entity;

            product.CreatedAt = DateTime.UtcNow;
            product.CreatedBy = "admin@openstore.com";

            await context.SaveChangesAsync();
        }

        List<ProductOptionDto> GetOptions(ProductCsvRow product)
        {
            var options = new List<ProductOptionDto>();

            if (!string.IsNullOrWhiteSpace(product.Attribute1Name))
            {
                options.Add(new ProductOptionDto
                {
                    Name = product.Attribute1Name,
                    Values = product.Attribute1Values?.Split("|").ToHashSet() ?? new HashSet<string>()
                });
            }

            if (!string.IsNullOrWhiteSpace(product.Attribute2Name))
            {
                options.Add(new ProductOptionDto
                {
                    Name = product.Attribute2Name,
                    Values = product.Attribute2Values?.Split("|").ToHashSet() ?? new HashSet<string>()
                });
            }

            if (!string.IsNullOrWhiteSpace(product.Attribute3Name))
            {
                options.Add(new ProductOptionDto
                {
                    Name = product.Attribute3Name,
                    Values = product.Attribute3Values?.Split("|").ToHashSet() ?? new HashSet<string>()
                });
            }

            if (!string.IsNullOrWhiteSpace(product.Attribute4Name))
            {
                options.Add(new ProductOptionDto
                {
                    Name = product.Attribute4Name,
                    Values = product.Attribute4Values?.Split("|").ToHashSet() ?? new HashSet<string>()
                });
            }

            if (!string.IsNullOrWhiteSpace(product.Attribute5Name))
            {
                options.Add(new ProductOptionDto
                {
                    Name = product.Attribute5Name,
                    Values = product.Attribute5Values?.Split("|").ToHashSet() ?? new HashSet<string>()
                });
            }

            return options;
        }
    }

    private async Task<IEnumerable<ProductMedia>> DownloadImage(ApplicationDbContext context, IList<string> urls, Guid variantID)
    {
        if (Download)
        {
            await Task.WhenAll(urls.Select(ImageDownloadHelper.Download));
        }
        
        return urls
            .Select(url => Path.GetFileName(new Uri(url).PathAndQuery))
            .Select(fileName =>
            {
                var path = $"/content/{fileName}";

                var media = context.ProductMedias.FirstOrDefault(x => x.Path == path);
                if (media is not null)
                {
                    return media;
                }

                return ProductMedia.Create(
                    "https://localhost:5001",
                    path,
                    "image/jpeg",
                    Path.GetExtension(fileName),
                    fileName,
                    0,
                    null,
                    Path.GetFileNameWithoutExtension(fileName)
                );
            })
            .ForEach(media => media.AddVariant(variantID))
            .Select(media =>
            {
                var existingMedia = context.ProductMedias.FirstOrDefault(x => x.Id == media.Id);
                return existingMedia ?? context.ProductMedias.Add(media).Entity;
            })
            .ToList();
    }

    private Dictionary<ProductCsvRow, List<ProductCsvRow>> GroupByParent(List<ProductCsvRow> productCsvRows)
    {
        var parents = productCsvRows.Where(x => string.IsNullOrWhiteSpace(x.Parent));

        var dictionary = productCsvRows
            .Where(x => !string.IsNullOrWhiteSpace(x.Parent))
            .GroupBy(x => x.Parent)
            .ToDictionary(x => productCsvRows.First(p => p.Sku == x.Key), x => x.ToList());

        var noChildProducts = parents.Where(p => !dictionary.ContainsKey(p));

        foreach (var noChildProduct in noChildProducts)
        {
            dictionary.Add(noChildProduct, new List<ProductCsvRow>());
        }

        return dictionary;
    }

    private static async Task<List<ProductCsvRow>> ExtractFromCsv(CancellationToken stoppingToken)
    {
        await using var csv = await CsvDataReader.CreateAsync("SampleProductDataSet.csv", new CsvDataReaderOptions
        {
            Culture = CultureInfo.InvariantCulture,
        });

        var id = csv.GetOrdinal("ID");
        var type = csv.GetOrdinal("Type");
        var sku = csv.GetOrdinal("SKU");
        var name = csv.GetOrdinal("Name");
        var shortDescription = csv.GetOrdinal("Short description");
        var description = csv.GetOrdinal("description");
        var taxStatus = csv.GetOrdinal("Tax status");
        var taxClass = csv.GetOrdinal("Tax class");
        var stock = csv.GetOrdinal("Stock");
        var weightLbs = csv.GetOrdinal("Weight (lbs)");
        var salePrice = csv.GetOrdinal("Sale price");
        var regularPrice = csv.GetOrdinal("Regular price");
        var categories = csv.GetOrdinal("Categories");
        var tags = csv.GetOrdinal("Tags");
        var images = csv.GetOrdinal("Images");
        var parent = csv.GetOrdinal("Parent");
        var attribute1Name = csv.GetOrdinal("Attribute 1 name");
        var attribute1Values = csv.GetOrdinal("Attribute 1 value(s)");
        var attribute2Name = csv.GetOrdinal("Attribute 2 name");
        var attribute2Values = csv.GetOrdinal("Attribute 2 value(s)");
        var attribute3Name = csv.GetOrdinal("Attribute 3 name");
        var attribute3Values = csv.GetOrdinal("Attribute 3 value(s)");
        var attribute4Name = csv.GetOrdinal("Attribute 4 name");
        var attribute4Values = csv.GetOrdinal("Attribute 4 value(s)");
        var attribute5Name = csv.GetOrdinal("Attribute 5 name");
        var attribute5Values = csv.GetOrdinal("Attribute 5 value(s)");

        var items = new List<ProductCsvRow>();
        while (await csv.ReadAsync(stoppingToken))
        {
            items.Add(
                new ProductCsvRow(
                    csv.GetStringOrDefault(id),
                    csv.GetEnum<ProductCsvRowType>(type),
                    csv.GetStringOrDefault(sku),
                    csv.GetStringOrDefault(name),
                    csv.GetStringOrDefault(shortDescription),
                    csv.GetStringOrDefault(description),
                    csv.GetStringOrDefault(taxStatus),
                    csv.GetStringOrDefault(taxClass),
                    csv.GetInt32(stock),
                    csv.GetDecimalOrDefault(weightLbs),
                    csv.GetDecimalOrDefault(salePrice),
                    csv.GetDecimal(regularPrice),
                    csv.GetStringOrDefault(categories),
                    csv.GetStringOrDefault(tags),
                    csv.GetStringOrDefault(images),
                    csv.GetStringOrDefault(parent),
                    csv.GetStringOrDefault(attribute1Name),
                    csv.GetStringOrDefault(attribute1Values),
                    csv.GetStringOrDefault(attribute2Name),
                    csv.GetStringOrDefault(attribute2Values),
                    csv.GetStringOrDefault(attribute3Name),
                    csv.GetStringOrDefault(attribute3Values),
                    csv.GetStringOrDefault(attribute4Name),
                    csv.GetStringOrDefault(attribute4Values),
                    csv.GetStringOrDefault(attribute5Name),
                    csv.GetStringOrDefault(attribute5Values),
                    Guid.NewGuid()
                )
            );
        }

        return items;
    }

    // ReSharper disable InconsistentNaming
    private enum ProductCsvRowType
    {
        simple,
        variable,
        variation
    }

    private record ProductCsvRow(
        string Id,
        ProductCsvRowType Type,
        string Sku,
        string Name,
        string ShortDescription,
        string Description,
        string TaxStatus,
        string TaxClass,
        int Stock,
        decimal? WeightLbs,
        decimal? SalePrice,
        decimal RegularPrice,
        string Categories,
        string Tags,
        string Images,
        string Parent,
        string Attribute1Name,
        string Attribute1Values,
        string Attribute2Name,
        string Attribute2Values,
        string Attribute3Name,
        string Attribute3Values,
        string Attribute4Name,
        string Attribute4Values,
        string Attribute5Name,
        string Attribute5Values,
        Guid Guid
    )
    {
        public override string ToString()
            => $"(Id={Id},Type={Type},Sku={Sku},Name={Name},Parent={Parent},SalePrice={SalePrice},RegularPrice={RegularPrice})";
    };
}