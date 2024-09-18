using InvoiceDiscountService.Repository;
using InvoiceDiscountService.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<CustomerDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));
        });
        services.AddScoped<ICustomerRepository, FakeCustomerRepository>();
        services.AddScoped<IInvoiceRepository, FakeInvoiceRepository>();
        services.AddScoped<IDiscountCalculator, DiscountCalculator>();
    })
    .Build();

host.Run();
