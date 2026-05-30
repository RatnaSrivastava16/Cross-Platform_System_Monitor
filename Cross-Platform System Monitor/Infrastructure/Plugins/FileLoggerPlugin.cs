using Cross_Platform_System_Monitor.Core.Interfaces;
using Cross_Platform_System_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross_Platform_System_Monitor.Infrastructure.Plugins
{
    public class FileLoggerPlugin : IMonitorPlugin
    {
        public async Task ExecuteAsync(SystemMetrics metrics)
        {
            string logMessage =
                $"[{metrics.Timestamp}] " +
                $"CPU: {metrics.CpuUsage:F2}% | " +
                $"RAM: {metrics.RamUsedMB:F2} MB | " +
                $"Disk: {metrics.DiskUsedGB:F2} GB";

            await File.AppendAllTextAsync("system_logs.txt", logMessage + Environment.NewLine);
        }
    }
}
