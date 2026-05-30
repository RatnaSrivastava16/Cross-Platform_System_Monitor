using Cross_Platform_System_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cross_Platform_System_Monitor.Core.Interfaces
{
    public interface IMonitorPlugin
    {
        Task ExecuteAsync(SystemMetrics metrics);
    }
}
