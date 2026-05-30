# Cross-Platform System Monitor

## Overview

Cross-Platform System Monitor is a C# console application that continuously monitors system resources such as CPU usage, memory usage, and disk usage. It follows a clean architecture approach and supports a plugin-based system, allowing external integrations like file logging and REST API reporting without modifying the core logic.

The application is designed to be extensible and cross-platform, with platform-specific monitoring abstracted behind interfaces.

---

## Features

- Real-time system monitoring (CPU, RAM, Disk)
- Configurable monitoring interval via `appsettings.json`
- Plugin-based architecture for extensibility
- Built-in plugins:
  - File Logger Plugin (logs system metrics locally)
  - API Plugin (sends metrics to REST endpoint)
- Clean Architecture with separation of concerns
- Dependency Injection using `Microsoft.Extensions.DependencyInjection`
- JSON-based configuration system

---

## Project Architecture

Cross-Platform-System-Monitor
│                                                                                                                                                                                                                   
├── Core                                                                                                                                                                                                            
│   ├── Interfaces                                                                                                                                                                                                  
│   │   ├── IPlatformMetricsProvider                                                                                                                                                                                
│   │   ├── IMonitorPlugin                                                                                                                                                                                          
│   ├── Models                                                                                                                                                                                                      
│                                                                                                                                                                                                                   
├── Infrastructure                                                                                                                                                                                                  
│   ├── Monitoring                                                                                                                                                                                                  
│   │   ├── WindowsMetricsProvider                                                                                                                                                                                  
│   ├── Plugins                                                                                                                                                                                                     
│   │   ├── FileLoggerPlugin                                                                                                                                                                                        
│   │   ├── ApiPlugin                                                                                                                                                                                               
│                                                                                                                                                                                                                   
├── Services                                                                                                                                                                                                        
│   │   ├── MonitorServices                                                                                                                                                                                         
│                                                                                                                                                                                                                   
├── Config                                                                                                                                                                                                          
│   ├── ApiSettings                                                                                                                                                                                                 
│   ├── MonitoringSettings                                                                                                                                                                                          
│                                                                                                                                                                                                                   
├── Program.cs                                                                                                                                                                                                      
├── appsettings.json                                                                                                                                                                                                  

---

## How It Works

1. Application starts from `Program.cs`
2. Configuration is loaded from `appsettings.json`
3. Dependency Injection registers:
   - Metrics provider
   - Plugins
   - Monitoring service
4. `MonitorServices` runs in a loop:
   - Collects system metrics
   - Sends data to all plugins
   - Displays metrics in console
   - Waits for configured interval

---

## Configuration

### appsettings.json                                                                                                                                                                                                

{
  "Monitoring": {
    "IntervalSeconds": 5
  },
  "ApiSettings": {
    "Endpoint": "-"
  }
}
---

## Plugins

### File Logger Plugin                                                                

###Logs system metrics into a local file:

---

### API Plugin

#### Sends system metrics to a REST API endpoint using HTTP POST.

Payload format:
{
  "cpu": 45.5,
  "ram_used": 1200,
  "disk_used": 51200
}
---

## Cross-Platform Design

#### System monitoring is abstracted using:

* `IPlatformMetricsProvider`

### Current Implementation

* WindowsMetricsProvider (uses PerformanceCounter)

### Future Support

* Linux (/proc filesystem)
* macOS (sysctl APIs)

#### This ensures the core logic remains unchanged when adding new platforms.

---

## How to Run

### Build
#### ctrl+shift+B

### Run
#### ctrl+f5

## Design Decisions

* Clean Architecture used for separation of concerns
* Plugin-based design enables extensibility without modifying core logic
* Dependency Injection ensures loose coupling and testability
* JSON configuration allows runtime flexibility without recompilation

---

## Challenges Faced

* Fetching accurate system-level memory and disk usage in a cross-platform way
* Handling Windows-specific APIs while maintaining platform-independent design
* Designing a scalable plugin system within limited time
* Managing continuous monitoring loop without blocking plugin execution

---

## Future Improvements

* Add Linux and macOS metrics providers
* Implement graceful shutdown using CancellationToken
* Add unit tests for services and plugins
* Improve API plugin with retry mechanism (Polly)
* Build a real-time dashboard (WPF / Blazor)

