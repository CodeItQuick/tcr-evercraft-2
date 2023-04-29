using EvercraftWebsite.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace tcr_evercraft_2_tests
{
    public class TestingWebAppFactory<T> : WebApplicationFactory<Program>
    {
        private readonly string _environment = "Development";
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            builder.ConfigureServices(services =>
            {
                var descriptors = services.Where(d =>
                    d.ServiceType == typeof(DbContextOptions<EvercraftDbContext>) ||
                    d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>))
                    .ToList();
                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                services.AddScoped(sp => new DbContextOptionsBuilder<EvercraftDbContext>()
                    .UseInMemoryDatabase("InMemoryDbForTesting")
                    .UseApplicationServiceProvider(sp)
                    .Options);
                services.AddScoped(sp => new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("Identity")
                    .UseApplicationServiceProvider(sp)
                    .Options);
            });
            return base.CreateHost(builder);
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                
                // antiforgery
                services.AddAntiforgery(t =>
                {
                    t.Cookie.Name = AntiForgeryTokenExtractor.Cookie;
                    t.FormFieldName = AntiForgeryTokenExtractor.Field;
                });

                var sp = services.BuildServiceProvider();
                sp.CreateScope();

            });
        }
    }
}