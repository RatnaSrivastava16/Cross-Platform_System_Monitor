using Cross_Platform_System_Monitor.Core.Interfaces;
using Cross_Platform_System_Monitor.Infrastructure.Monitoring;
using Cross_Platform_System_Monitor.Infrastructure.Plugins;
using Cross_Platform_System_Monitor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Cross_Platform_System_Monitor
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string endpoint = configuration["ApiSettings:Endpoint"] ?? "";

            int interval = int.Parse(configuration["Monitoring:IntervalSeconds"] ?? "5");
            Console.WriteLine($"API Endpoint: {endpoint}");
            Console.WriteLine($"Interval: {interval}");

            var services = new ServiceCollection();

            services.AddSingleton<IPlatformMetricsProvider, WindowsMetricsProvider>();

            services.AddSingleton<IMonitorPlugin, FileLoggerPlugin>();
            services.AddSingleton<IMonitorPlugin>(
                provider => new ApiPlugin(endpoint));
            services.AddSingleton<MonitorServices>(
                provider => new MonitorServices(
                    provider.GetRequiredService<IPlatformMetricsProvider>(),
                    provider.GetServices<IMonitorPlugin>(),
                    interval
                ));
            var serviceProvider = services.BuildServiceProvider();

            var monitorService =
                serviceProvider.GetRequiredService<MonitorServices>();

            await monitorService.StartMonitoringAsync();
        }
    }
}