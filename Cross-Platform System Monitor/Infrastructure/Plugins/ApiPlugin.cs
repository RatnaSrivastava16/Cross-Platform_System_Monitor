using Cross_Platform_System_Monitor.Core.Interfaces;
using Cross_Platform_System_Monitor.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Json;


namespace Cross_Platform_System_Monitor.Infrastructure.Plugins
{
    public class ApiPlugin: IMonitorPlugin
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public ApiPlugin(string endpoint)
        {
            _httpClient = new HttpClient();
            _endpoint = endpoint;
        }
        public async Task ExecuteAsync(SystemMetrics metrics)
        {
            try
            {
                var payload = new
                {
                    cpu = metrics.CpuUsage,
                    ram_used = metrics.RamUsedMB,
                    disk_used = metrics.DiskUsedGB
                };

                var response = await _httpClient.PostAsJsonAsync(_endpoint,payload);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Success: {response.IsSuccessStatusCode}");

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Success: {response.IsSuccessStatusCode}");

                Console.WriteLine($"Length: {result.Length}");

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }
    }
}
