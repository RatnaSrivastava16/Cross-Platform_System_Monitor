using Cross_Platform_System_Monitor.Core.Interfaces;
using Cross_Platform_System_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross_Platform_System_Monitor.Services
{
    public class MonitorServices
    {
        private readonly IPlatformMetricsProvider _metricsProvider;
        private readonly IEnumerable<IMonitorPlugin> _plugins;
        private readonly int _intervalSeconds;

        public MonitorServices(IPlatformMetricsProvider metricsProvider,IEnumerable<IMonitorPlugin> plugins, int intervalSeconds)
        {
            _metricsProvider = metricsProvider;
            _plugins = plugins;
            _intervalSeconds = intervalSeconds;
        }
        public async Task StartMonitoringAsync()
        {
            while(true)
            {
                try
                {
                    SystemMetrics metrics = await _metricsProvider.GetMetricsAsync();
                    foreach (var plugin in _plugins)
                    {
                        await plugin.ExecuteAsync(metrics);
                    }
                    Console.Clear();

                    Console.WriteLine("===== SYSTEM MONITOR =====");

                    Console.WriteLine($"Time: {metrics.Timestamp}");

                    Console.WriteLine($"CPU Usage: {metrics.CpuUsage:F2}%");

                    Console.WriteLine($"RAM Usage: {metrics.RamUsedMB:F2} MB / {metrics.RamTotalMB:F2} MB");

                    Console.WriteLine($"Disk Usage: {metrics.DiskUsedGB:F2} GB / {metrics.DiskTotalGB:F2} GB");

                    Console.WriteLine("==========================");

                    await Task.Delay(_intervalSeconds*1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
