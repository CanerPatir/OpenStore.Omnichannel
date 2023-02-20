using Microsoft.EntityFrameworkCore;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;
using Sylvan.Data.Csv;

namespace OpenStore.Omnichannel.Tools;

public class AddressImporter : BackgroundService
{
    private readonly ILogger<AddressImporter> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AddressImporter(ILogger<AddressImporter> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    record CsvRow(string City, decimal Lat, decimal Lng, string Country, string Iso2, string Capital, string AdminName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
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

        // ref: https://www.kaggle.com/juanmah/world-cities
        // Blank string if not a capital, otherwise: - primary - country's capital (e.g. Washington D.C.) - admin - first-level admin
        await using var csv = await CsvDataReader.CreateAsync("WorldCities.csv");

        // "city","city_ascii","lat","lng","country","iso2","iso3","admin_name","capital","population","id"
        var city = csv.GetOrdinal("city");
        var cityAscii = csv.GetOrdinal("city_ascii");
        var lat = csv.GetOrdinal("lat");
        var lng = csv.GetOrdinal("lng");
        var country = csv.GetOrdinal("country");
        var iso2 = csv.GetOrdinal("iso2");
        var iso3 = csv.GetOrdinal("iso3");
        var adminName = csv.GetOrdinal("admin_name");
        var capital = csv.GetOrdinal("capital");

        var items = new List<CsvRow>();
        while (await csv.ReadAsync(stoppingToken))
        {
            items.Add(
                new CsvRow(
                    csv.GetString(city),
                    csv.GetDecimal(lat),
                    csv.GetDecimal(lat),
                    csv.GetString(country),
                    csv.GetString(iso2),
                    csv.GetString(capital),
                    csv.GetString(adminName)
                )
            );
        }


        var a = items.Where(x => x.Country == "Turkey" && x.Capital is "admin" or "primary").Select(x => x.City).OrderBy(x => x).ToList();

        Console.WriteLine();
    }
}