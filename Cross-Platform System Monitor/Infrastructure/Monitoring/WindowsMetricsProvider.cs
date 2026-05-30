using Cross_Platform_System_Monitor.Core.Interfaces;
using Cross_Platform_System_Monitor.Core.Models;
using System.Diagnostics;
using Microsoft.VisualBasic;
namespace Cross_Platform_System_Monitor.Infrastructure.Monitoring
{
    public class WindowsMetricsProvider : IPlatformMetricsProvider
    {
        private readonly PerformanceCounter _cpuCounter;

        public WindowsMetricsProvider()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            // First call usually returns 0
            _cpuCounter.NextValue();

            Thread.Sleep(1000);
        }

        public async Task<SystemMetrics> GetMetricsAsync()
        {
            float cpuUsage = _cpuCounter.NextValue();

            // RAM Info
            var gcInfo = GC.GetGCMemoryInfo();

            double totalRam = gcInfo.TotalAvailableMemoryBytes / (1024.0 * 1024.0);

            double usedRam = GC.GetTotalMemory(false) / (1024.0 * 1024.0);

            // Disk Info
            DriveInfo drive = DriveInfo.GetDrives()
                .FirstOrDefault(d => d.IsReady && d.Name == "C:\\");

            double totalDisk = drive.TotalSize / (1024.0 * 1024.0 * 1024.0);

            double freeDisk = drive.TotalFreeSpace / (1024.0 * 1024.0 * 1024.0);

            double usedDisk = totalDisk - freeDisk;

            return new SystemMetrics
            {
                CpuUsage = cpuUsage,
                RamUsedMB = usedRam,
                RamTotalMB = totalRam,
                DiskUsedGB = usedDisk,
                DiskTotalGB = totalDisk,
                Timestamp = DateTime.Now
            };
        }
    }
}