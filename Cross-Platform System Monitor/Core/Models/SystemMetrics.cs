using System;
using System.Collections.Generic;
using System.Text;

namespace Cross_Platform_System_Monitor.Core.Models
{
    public class SystemMetrics
    {
        public float CpuUsage { get; set; }
        public double RamUsedMB { get; set; }
        public double RamTotalMB { get; set; }
        public double DiskUsedGB { get; set; }
        public double DiskTotalGB { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
