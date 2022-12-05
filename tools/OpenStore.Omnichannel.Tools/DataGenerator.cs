using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;
using OpenStore.Omnichannel.Shared.Command.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Tools;

public class DataGenerator : BackgroundService
{
    private readonly ILogger<DataGenerator> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DataGenerator(ILogger<DataGenerator> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //     await Task.Delay(1000, stoppingToken);
        // }
        using var scope = _serviceProvider.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync(stoppingToken);
        try
        {
            if (pendingMigrations.Any()) await context.Database.MigrateAsync(stoppingToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        await GenerateProducts(context, stoppingToken);
    }

    private static async Task GenerateProducts(ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var faker = new ProductDtoFaker();

        for (var i = 0; i < 100; i++)
        {
            var product = Product.Create(new CreateProduct(faker.Generate()), id => context.ProductMedias.Single(x => x.Id == id));

            await context.Products.AddAsync(product, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private sealed class ProductDtoFaker : Faker<ProductDto>
    {
        public ProductDtoFaker()
        {
            RuleFor(o => o.Title, f => f.Commerce.ProductName());
            RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
            RuleFor(o => o.Handle, f => f.Commerce.ProductAdjective());
        }
    }
}